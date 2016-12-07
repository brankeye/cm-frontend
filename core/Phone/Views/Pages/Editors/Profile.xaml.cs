using cm.frontend.core.Phone.ViewModels.Base;
using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Profile
    {
        private Base.PageController<ViewModels.Pages.Editors.Profile> PageController { get; }

        public Profile()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Editors.Profile>(this);
        }

        public void Initialize(bool isEditingNewProfile)
        {
            PageController.ViewModel.Initialize(isEditingNewProfile);
        }
    }
}
