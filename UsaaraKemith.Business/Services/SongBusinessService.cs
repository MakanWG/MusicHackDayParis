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
    public class SongBusinessService : ISongBusinessService
    {
        private ISongClientService _songClientService;
        public SongBusinessService(ISongClientService songClientService)
        {
            _songClientService = songClientService;
        }
        public IObservable<IEnumerable<Track>> GetPlaylistsTracks()
        {
            return _songClientService.GetPlaylistsTracks();
        }
    }
}
