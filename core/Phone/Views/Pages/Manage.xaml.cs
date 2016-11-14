using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Manage : ContentPage
    {
        public ViewModels.Pages.Manage ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Manage);
        private ViewModels.Pages.Manage _vm;

        public Manage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Manage();
        }
    }
}
