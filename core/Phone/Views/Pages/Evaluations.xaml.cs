using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Evaluations : ContentPage
    {
        public ViewModels.Pages.Evaluations ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Evaluations);
        private ViewModels.Pages.Evaluations _vm;

        public Evaluations()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Evaluations();
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
