using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class EvaluationSection
    {
        private Base.PageController<ViewModels.Pages.Editors.EvaluationSection> PageController { get; }

        public EvaluationSection()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Editors.EvaluationSection>(this);
        }

        public void Initialize(int sectionLocalId, int evaluationLocalId, bool isEditingExistingSection)
        {
            PageController.ViewModel.Initialize(sectionLocalId, evaluationLocalId, isEditingExistingSection);
        }
    }
}
