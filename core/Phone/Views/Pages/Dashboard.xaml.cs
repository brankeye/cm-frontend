using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Dashboard : ContentPage
    {
        public ViewModels.Pages.Dashboard ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Dashboard);
        private ViewModels.Pages.Dashboard _vm;

        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Dashboard();
        }
    }
}
