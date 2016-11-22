namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class EvaluationSection
    {
        public EvaluationSection()
        {
            InitializeComponent();
        }

        public void Initialize(int sectionLocalId, int evaluationLocalId, bool isNewSection)
        {
            ViewModel.Initialize(sectionLocalId, evaluationLocalId, isNewSection);
        }
    }
}
