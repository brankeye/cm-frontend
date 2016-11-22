using System;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Base
{
    public class Core<TViewModel> : ContentPage
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = BindingContext as TViewModel);
        private TViewModel _vm;

        public Core()
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
