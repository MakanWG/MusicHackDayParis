using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Reactive;
using UsaaraKemith.Client.Contracts;
using UsaaraKemith.Entities;
using WG.Extensions.WinPRT.Query;
using WG.Network.WinPRT.Query;

namespace UsaaraKemith.Client.Services
{
    public class SongClientService : ISongClientService
    {

        private Subject<IEnumerable<Track>> _getPlaylistsSubject { get; set; }

        public SongClientService()
        {
            _getPlaylistsSubject = new Subject<IEnumerable<Track>>();
        }
        public IObservable<IEnumerable<Track>> GetPlaylistsTracks()
        {
            WGRestRequest requestObject = new WGRestRequest();
            requestObject.SetUri(Constants.SoundCloud.BaseUri)
                .Get()
                .AppendPath("playlists.json")
                .Param("client_id", Constants.SoundCloud.ClientId)
                .ToResponseEntity<List<Playlist>>((response, status, code,content) =>
                {
                    if (response != null)
                    {
                        _getPlaylistsSubject.OnNext(response.SelectMany(playlist => playlist.Tracks.Select(track =>
                        {
                            track.Type = "playlist";
                            track.AssociatedPlaylist = playlist.Title.Replace("Electropose", "Electro Posé").Replace("Electroposé", "Electro Posé");
                            return track;
                        })));
                        _getPlaylistsSubject.OnCompleted();
                    }
                    else
                    {
                        _getPlaylistsSubject.OnError(new Exception());
                    }

                });

            return _getPlaylistsSubject;
        }
    }
}
