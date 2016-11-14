using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Profile : ContentPage
    {
        public ViewModels.Pages.Editors.Profile ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.Profile);
        private ViewModels.Pages.Editors.Profile _vm;

        public Profile()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.Profile();
        }
    }
}
