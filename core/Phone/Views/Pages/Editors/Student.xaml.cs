using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Student : ContentPage
    {
        public ViewModels.Pages.Editors.Student ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.Student);
        private ViewModels.Pages.Editors.Student _vm;

        public Student()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.Student();
        }
    }
}
