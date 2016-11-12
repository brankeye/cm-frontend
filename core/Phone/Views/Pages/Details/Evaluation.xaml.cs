using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Evaluation : ContentPage
    {
        public ViewModels.Pages.Details.Evaluation ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.Evaluation);
        private ViewModels.Pages.Details.Evaluation _vm;

        public Evaluation()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.Evaluation();
        }
    }
}
