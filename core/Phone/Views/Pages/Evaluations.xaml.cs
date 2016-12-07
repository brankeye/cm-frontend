using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Evaluations
    {
        private Base.PageController<ViewModels.Pages.Evaluations> PageController { get; }

        public Evaluations()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Evaluations>(this);
        }

        public void Initialize(int profileLocalId)
        {
            PageController.ViewModel.Initialize(profileLocalId);
        }

        private void Evaluations_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation) e.SelectedItem;
            PageController.ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
