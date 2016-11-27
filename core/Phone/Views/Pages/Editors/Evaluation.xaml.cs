namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            InitializeComponent();
        }

        public void InitializeExisting(int evaluationLocalId)
        {
            ViewModel.InitializeExisting(evaluationLocalId);
        }

        public void InitializeNew(int memberLocalId)
        {
            ViewModel.InitializeNew(memberLocalId);
        }
    }
}
