using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Evaluations : ContentPage
    {
        public ViewModels.Pages.Evaluations ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Evaluations);
        private ViewModels.Pages.Evaluations _vm;

        public Evaluations()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Evaluations();
        }
    }
}
