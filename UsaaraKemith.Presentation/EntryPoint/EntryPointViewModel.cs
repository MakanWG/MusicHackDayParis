using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WG.Presentation.WinPRT.ViewModels;

namespace UsaaraKemith.Presentation.EntryPoint
{
    public class EntryPointViewModel : ViewModelBase
    {
        public void SetApp()
        {
            //if (this.BusinessServices().Login.GetUserCredentials()!=null)
            //{
            //    this.Services().Navigation.NavigateToHome();
            //    return;
            //}
            this.Services().Navigation.NavigateToHome();
        }

        public override void Charged()
        {

        }

        public override void Uncharged()
        {
        }
    }
}
