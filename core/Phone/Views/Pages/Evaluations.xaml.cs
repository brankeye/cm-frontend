using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Evaluations
    {
        public Evaluations()
        {
            InitializeComponent();
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
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation) e.SelectedItem;
            ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
