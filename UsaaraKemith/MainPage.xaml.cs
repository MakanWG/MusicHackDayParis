using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UsaaraKemith.Resources;
using UsaaraKemith.Presentation;
using UsaaraKemith.Presentation.EntryPoint;

namespace UsaaraKemith
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += (se, e) =>
            {
                Initializer.SetApp(Application.Current);
                this.DataContext = new EntryPointViewModel();
                var vm = (EntryPointViewModel)DataContext;
                vm.SetApp();
                this.Loaded += (s, le) => vm.SetApp();
            };
        }
    }
}