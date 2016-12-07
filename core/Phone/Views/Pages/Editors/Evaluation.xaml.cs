using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Evaluation
    {
        private Base.PageController<ViewModels.Pages.Editors.Evaluation> PageController { get; }

        public Evaluation()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Editors.Evaluation>(this);
        }

        public void InitializeExisting(int evaluationLocalId)
        {
            PageController.ViewModel.InitializeExisting(evaluationLocalId);
        }

        public void InitializeNew(int memberLocalId)
        {
            PageController.ViewModel.InitializeNew(memberLocalId);
        }
    }
}
