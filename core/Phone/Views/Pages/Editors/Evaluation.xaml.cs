using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Evaluation : ContentPage
    {
        public ViewModels.Pages.Editors.Evaluation ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.Evaluation);
        private ViewModels.Pages.Editors.Evaluation _vm;

        public Evaluation()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.Evaluation();
        }
    }
}
