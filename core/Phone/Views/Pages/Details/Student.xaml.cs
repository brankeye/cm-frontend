using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Student : ContentPage
    {
        public ViewModels.Pages.Details.Student ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.Student);
        private ViewModels.Pages.Details.Student _vm;

        public Student()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.Student();
        }
    }
}
