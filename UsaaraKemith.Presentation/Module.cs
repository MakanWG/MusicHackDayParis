using Ninject.Modules;
using UsaaraKemith.Business.Contracts;
using UsaaraKemith.Business.Services;
using UsaaraKemith.Client.Contracts;
using UsaaraKemith.Client.Services;
using UsaaraKemith.Presentation.Home;
using UsaaraKemith.Presentation.ServicesLocators;
using UsaaraKemith.Views.Home;
using WG.Presentation.WinPRT.Navigation;
using WG.Views.WinPRT;

namespace UsaaraKemith.Presentation
{
    public class Module : NinjectModule
    {
        public PageDeclaration[] Pages { get; set; }

        private void RegisterLocators()
        {
            Bind<ServicesLocator>().ToSelf().InSingletonScope();
            Bind<ClientServicesLocator>().ToSelf().InSingletonScope();
            Bind<BusinessServicesLocator>().ToSelf().InSingletonScope();
        }

        private void RegisterServices()
        {
            Bind<NavigationController>().ToSelf().InSingletonScope();
            Bind<ISettingsService>().To<SettingsService>().InSingletonScope();
            Bind<ISongClientService>().To<SongClientService>().InSingletonScope();
            Bind<ISongBusinessService>().To<SongBusinessService>().InSingletonScope();
            Bind<IAnalyzisClientService>().To<AnalyzisClientService>().InSingletonScope();
            Bind<IAnalyzisBusinessService>().To<AnalyzisBusinessService>().InSingletonScope();
        }

        private void RegisterPages()
        {
            Pages = new[]
            {
                new PageDeclaration
                {
                    ApplicationPage = ApplicationPages.Home,
                    Uri = "/Home/HomePage.xaml",
                    ViewModel = new HomePageViewModel(),
                    PageType = typeof (HomePage)
                }
            };
        }

        public override void Load()
        {
            RegisterPages();
            RegisterLocators();
            RegisterServices();
        }
    }
}