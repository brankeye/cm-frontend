namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            InitializeComponent();
        }

        public void Initialize(int studentLocalId)
        {
            ViewModel.Initialize(studentLocalId);
        }

        public void Initialize(int evalLocalId, int studentLocalId)
        {
            ViewModel.Initialize(studentLocalId, evalLocalId);
        }
    }
}
