using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Class
    {
        private Base.PageController<ViewModels.Pages.Editors.Class> PageController { get; }

        public Class()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Editors.Class>(this);
    }

        public void Initialize(int classLocalId)
        {
            PageController.ViewModel.Initialize(classLocalId);
        }
    }
}
