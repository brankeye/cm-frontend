using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Base
{
    public class ContentCore<TViewModel> : ContentPage
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = BindingContext as TViewModel);
        private TViewModel _vm;

        public ContentCore()
        {
            BindingContext = new TViewModel();
            SetBinding(NavigationProperty, new Binding("Navigation"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            ViewModel.OnDisappearing();
        }
    }

    public class MasterDetailCore<TViewModel> : MasterDetailPage
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = BindingContext as TViewModel);
        private TViewModel _vm;

        public MasterDetailCore()
        {
            BindingContext = new TViewModel();
            SetBinding(NavigationProperty, new Binding("Navigation"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            ViewModel.OnDisappearing();
        }
    }
}
