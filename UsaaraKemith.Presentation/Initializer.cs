using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using UsaaraKemith.Presentation.ServicesLocators;

namespace UsaaraKemith.Presentation
{
    public static class Initializer
    {
        private static Application _app;
        private static Module _module;
        private static IKernel _kernel;
        public static Dictionary<string, object> LocatorsStore { get; set; }
        public static void InitializeInjection()
        {
            _kernel = new StandardKernel();
            _module = new Module();
            _kernel.Load(_module);
            LoadLocators();
        }

        private static void LoadLocators()
        {
            LocatorsStore = new Dictionary<string, object>
            {
                {"ServicesLocator", _kernel.Get<ServicesLocator>()},
                {"ClientServicesLocator", _kernel.Get<ClientServicesLocator>()},
                {"BusinessServicesLocator", _kernel.Get<BusinessServicesLocator>()}
            };
        }

        public static Module GetModule()
        {
            return _module;
        }

        public static void SetApp(Application app)
        {
            _app = app;
        }

        public static Application GetApp()
        {
            return _app;
        }
    }
}
