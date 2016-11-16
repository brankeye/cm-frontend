using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Classes : ContentPage
    {
        public ViewModels.Pages.Classes ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Classes);
        private ViewModels.Pages.Classes _vm;

        public Classes()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Classes();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
