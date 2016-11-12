using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Class : ContentPage
    {
        public ViewModels.Pages.Details.Class ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.Class);
        private ViewModels.Pages.Details.Class _vm;

        public Class()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.Class();
        }
    }
}
