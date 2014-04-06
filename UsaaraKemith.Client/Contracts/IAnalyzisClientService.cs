using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Entities;

namespace UsaaraKemith.Client.Contracts
{
    public interface IAnalyzisClientService
    {
        IObservable<SongAnalysis> Analyzis(string songLink);
    }
}
