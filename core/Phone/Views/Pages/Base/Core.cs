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
            SetBinding(ContentPage.NavigationProperty, new Binding("Navigation"));
        }
    }
}
