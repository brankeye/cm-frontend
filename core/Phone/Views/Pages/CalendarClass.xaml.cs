using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class CalendarClass : ContentPage
    {
        public ViewModels.Pages.CalendarClass ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.CalendarClass);
        private ViewModels.Pages.CalendarClass _vm;

        public CalendarClass()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.CalendarClass();
        }

        public void Initialize(int classLocalId, DateTimeOffset date)
        {
            ViewModel.Initialize(classLocalId, date);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
