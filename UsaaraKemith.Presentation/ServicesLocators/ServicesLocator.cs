using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WG.Presentation.WinPRT.Navigation;


namespace UsaaraKemith.Presentation.ServicesLocators
{
    public class ServicesLocator
    {
        public ServicesLocator(NavigationController navigationController)
        {
            _navigation = navigationController;
        }
        private NavigationController _navigation;

        public NavigationController Navigation
        {
            get { return _navigation; }
        }
    }
}
