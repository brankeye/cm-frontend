using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Class : ContentPage
    {
        public ViewModels.Pages.Editors.Class ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.Class);
        private ViewModels.Pages.Editors.Class _vm;

        public Class()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.Class();
        }

        public void Initialize(int classLocalId)
        {
            ViewModel.Initialize(classLocalId);
        }
    }
}
