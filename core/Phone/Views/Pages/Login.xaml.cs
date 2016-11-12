using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Login : ContentPage
    {
        public ViewModels.Pages.Login ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Login);
        private ViewModels.Pages.Login _vm;

        public Login()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Login();
        }
    }
}
