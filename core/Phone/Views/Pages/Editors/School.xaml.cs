using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class School : ContentPage
    {
        public ViewModels.Pages.Editors.School ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.School);
        private ViewModels.Pages.Editors.School _vm;

        public School()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.School();
        }
    }
}
