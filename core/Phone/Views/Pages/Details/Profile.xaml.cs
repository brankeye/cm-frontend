using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Profile : ContentPage
    {
        public ViewModels.Pages.Details.Profile ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.Profile);
        private ViewModels.Pages.Details.Profile _vm;

        public Profile()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.Profile();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
