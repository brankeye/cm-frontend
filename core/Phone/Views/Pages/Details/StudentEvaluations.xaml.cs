using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class StudentEvaluations
    {
        public StudentEvaluations()
        {
            InitializeComponent();
            var header = (View) EvaluationsListView.Header;
            header.BindingContext = BindingContext;
        }

        public void Initialize(int profileLocalId)
        {
            ViewModel.Initialize(profileLocalId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void Evaluations_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation)e.SelectedItem;
            ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
