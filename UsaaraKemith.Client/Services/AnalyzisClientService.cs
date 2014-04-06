using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Client.Contracts;
using UsaaraKemith.Entities;
using WG.Extensions.WinPRT.Query;
using WG.Network.WinPRT.Query;

namespace UsaaraKemith.Client.Services
{
    public class AnalyzisClientService : IAnalyzisClientService
    {
        private ReplaySubject<SongAnalysis> _analyzisSubject;
        public AnalyzisClientService()
        {
            _analyzisSubject = new ReplaySubject<SongAnalysis>();
        }

        public IObservable<SongAnalysis> Analyzis(string songLink)
        {
            var restRequest = new WGRestRequest();
            restRequest.SetUri(Constants.EchoNest.BaseUri)
                .Post()
                .AppendPath("upload")
                .Param("api_key", Constants.EchoNest.APIKey)
                .Param("url", songLink)
                .AddHeader("Content-Type", "multipart/form-data")
                .ToResponseEntity<EchoNestResponseWrapper>((response, status, code, content) =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(10)).Subscribe(_ =>
                    {
                        GetAnalyzisLink(response.Response.Track.Id);
                    });

                });
            return _analyzisSubject;
        }

        private void GetAnalyzisLink(string songId)
        {
            var restRequest = new WGRestRequest();
            restRequest.SetUri(Constants.EchoNest.BaseUri)
                .Get()
                .AppendPath("profile")
                .Param("api_key", Constants.EchoNest.APIKey)
                .Param("format", "json")
                .Param("id", songId)
                .Param("bucket", "audio_summary")
                .ToResponseEntity<EchoNestResponseWrapper>((response, status, code, content) =>
                {
                    ReturnAnalyze(response.Response.Track.AudioSummary.AnalysisUrl);
                });
        }

        private void ReturnAnalyze(string analyzisUrl)
        {
             var restRequest = new WGRestRequest();
            restRequest.SetUri(analyzisUrl)
                .Get()
                .ToResponseEntity<SongAnalysis>((response, status, code, content) =>
                {
                    
                    _analyzisSubject.OnNext(response);
                });

        }
    }
}
