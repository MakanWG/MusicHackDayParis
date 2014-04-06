using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Entities;

namespace UsaaraKemith.Business.Contracts
{
    public interface IAnalyzisBusinessService
    {
        IObservable<SongAnalysis> Analyzis(string songLink);
    }
}
