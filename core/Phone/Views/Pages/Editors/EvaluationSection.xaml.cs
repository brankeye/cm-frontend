namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class EvaluationSection
    {
        public EvaluationSection()
        {
            InitializeComponent();
        }

        public void Initialize(int sectionLocalId, int evaluationLocalId, bool isEditingExistingSection)
        {
            ViewModel.Initialize(sectionLocalId, evaluationLocalId, isEditingExistingSection);
        }
    }
}
