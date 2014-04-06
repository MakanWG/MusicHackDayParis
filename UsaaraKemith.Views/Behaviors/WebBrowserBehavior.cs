using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;

namespace UsaaraKemith.Views.Behaviors
{
    public class WebBrowserBehavior : Behavior<WebBrowser>
    {
        public static DependencyProperty BrowserClosedCommandProperty =
           DependencyProperty.Register("BrowserClosedCommand", typeof(ICommand), typeof(WebBrowserBehavior), new PropertyMetadata(default(ICommand)));

        public ICommand BrowserClosedCommand
        {
            get { return (ICommand)GetValue(BrowserClosedCommandProperty); }
            set { SetValue(BrowserClosedCommandProperty, value); }
        }

        public static DependencyProperty FinalUriProperty =
           DependencyProperty.Register("FinalUri", typeof(string), typeof(WebBrowserBehavior), new PropertyMetadata(default(string)));

        public string FinalUri
        {
            get { return (string)GetValue(FinalUriProperty); }
            set { SetValue(FinalUriProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Navigating += BrowserNavigating;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Navigating -= BrowserNavigating;
        }

        void BrowserNavigating(object sender, NavigatingEventArgs e)
        {
                BrowserClosedCommand.Execute(e);
        }
    }
}
