using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Business.Contracts;
using UsaaraKemith.Client.Contracts;
using UsaaraKemith.Entities;

namespace UsaaraKemith.Business.Services
{
    public class AnalyzisBusinessService : IAnalyzisBusinessService
    {
        private IAnalyzisClientService _analyzisClientService;
        public AnalyzisBusinessService(IAnalyzisClientService analyzisClientService)
        {
            _analyzisClientService = analyzisClientService;
        }
        public IObservable<SongAnalysis> Analyzis(string songLink)
        {
            return _analyzisClientService.Analyzis(string.Format("{0}?client_id={1}", songLink,
                UsaaraKemith.Client.Constants.SoundCloud.ClientId));
        }
    }
}
