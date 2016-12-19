using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class School
    {
        private Base.PageController<ViewModels.Pages.Editors.School> PageController { get; }

        public School()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Editors.School>(this);
        }

        public void Initialize(string schoolName, bool isManaging)
        {
            PageController.ViewModel.Initialize(schoolName, isManaging);
        }
    }
}
