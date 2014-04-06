using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Business.Contracts;

namespace UsaaraKemith.Presentation.ServicesLocators
{
    public class BusinessServicesLocator
    {
        private IAnalyzisBusinessService _analyzisBusinessService;
        private ISongBusinessService _songBusinessService;
        public BusinessServicesLocator( IAnalyzisBusinessService analyzisBusinessService, ISongBusinessService songBusinessService)
        {
            
            _analyzisBusinessService = analyzisBusinessService;
            _songBusinessService = songBusinessService;
        }

        public IAnalyzisBusinessService Analyzis
        {
            get { return _analyzisBusinessService; }
        }

        public ISongBusinessService Song
        {
            get { return _songBusinessService; }
        }
    }
}
