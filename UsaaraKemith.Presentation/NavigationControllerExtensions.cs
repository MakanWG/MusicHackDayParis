using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WG.Presentation.WinPRT.Navigation;
using WG.Presentation.WinPRT.ViewModels;
using WG.Views.WinPRT;

namespace UsaaraKemith.Presentation
{
    public static class NavigationControllerExtensions
    {
        private static EventHandler<CancelEventArgs> _handler;
        public const string ViewsAssembly = "/UsaaraKemith.Views;component";
        public static PageDeclaration CurrentPage;


        public static void NavigateTo(this NavigationController controller, ApplicationPages page)
        {
            var module = (Module)Initializer.GetModule();
            CurrentPage = module.Pages
                .Select(p => p).Single(p => (ApplicationPages)p.ApplicationPage == page);
            var currentRoot = (PhoneApplicationFrame)Initializer.GetApp().RootVisual;
            _handler += (s, e) =>
            {
                var currentVm = (ViewModelBase)CurrentPage.ViewModel;
                currentVm.Uncharged();
                currentRoot.BackKeyPress -= _handler;
            };
            currentRoot.BackKeyPress += _handler;
            controller.NavigateTo(String.Format("{0}{1}", ViewsAssembly, CurrentPage.Uri), currentRoot, SetViewModel);
        }

        public static void NavigateTo(this NavigationController controller, ApplicationPages page, Tuple<string, object> tuple)
        {
            var module = (Module)Initializer.GetModule();
            CurrentPage = module.Pages
                .Select(p => p).Single(p => (ApplicationPages)p.ApplicationPage == page);
            var app = Initializer.GetApp();
            SetParameter(tuple);
            var currentRoot = (PhoneApplicationFrame)app.RootVisual;
            _handler += (s, e) =>
            {
                var currentVm = (ViewModelBase)CurrentPage.ViewModel;
                currentVm.Uncharged();
                currentRoot.BackKeyPress -= _handler;
            };
            currentRoot.BackKeyPress += _handler;
            controller.NavigateTo(String.Format("{0}{1}", ViewsAssembly, CurrentPage.Uri), currentRoot, SetViewModel);
        }

        private static object GetParameter(string key)
        {
            var parameter = Initializer
                                .GetApp()
                                .Resources
                                .Select(o => o)
                                .Single(o => o.Key.ToString() == key)
                                .Value;
            return parameter;
        }

        private static void SetParameter(Tuple<string, object> parameter)
        {
            var resources = Initializer.GetApp().Resources;

            if (resources.Contains(parameter.Item1))
            {
                resources.Remove(parameter.Item1);
                resources.Add(parameter.Item1, parameter.Item2);
            }
            else
                resources.Add(parameter.Item1, parameter.Item2);
        }

        public static void SetViewModel(PhoneApplicationPage root)
        {
            root.DataContext = CurrentPage.ViewModel;
            var vm = (ViewModelBase)CurrentPage.ViewModel;
            vm.Charged();
        }

        public static void NavigateToHome(this NavigationController controller)
        {
            NavigateTo(controller, ApplicationPages.Home);
        }
    }
}
