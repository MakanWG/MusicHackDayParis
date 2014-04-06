using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WG.Presentation.WinPRT.ViewModels;
using UsaaraKemith.Presentation.ServicesLocators;

namespace UsaaraKemith.Presentation
{
    public static class ViewModelExtensions
    {
        public static ServicesLocator Services(this ViewModelBase vmbase)
        {
            var resources = Initializer.LocatorsStore;
            var locator = resources["ServicesLocator"] as ServicesLocator;
            return locator;
        }

        public static ClientServicesLocator ClientServices(this ViewModelBase vmbase)
        {
            var resources = Initializer.LocatorsStore;
            var locator = resources["ClientServicesLocator"] as ClientServicesLocator;
            return locator;
        }

        public static BusinessServicesLocator BusinessServices(this ViewModelBase vmbase)
        {
            var resources = Initializer.LocatorsStore;
            var locator = resources["BusinessServicesLocator"] as BusinessServicesLocator;
            return locator;
        }
    }
}
