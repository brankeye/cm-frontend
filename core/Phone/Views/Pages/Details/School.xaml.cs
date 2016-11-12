using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class School : ContentPage
    {
        public ViewModels.Pages.Details.School ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.School);
        private ViewModels.Pages.Details.School _vm;

        public School()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.School();
        }
    }
}
