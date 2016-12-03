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
            
            if (ViewModel.IsTeacher())
            {
                var editEvaluation = new ToolbarItem
                {
                    Text = "Add",
                    Order = ToolbarItemOrder.Primary
                };
                editEvaluation.SetBinding(MenuItem.CommandProperty, new Binding("AddCommand"));

                ToolbarItems.Add(editEvaluation);
            }
        }

        public void Initialize(int profileLocalId)
        {
            ViewModel.Initialize(profileLocalId);
        }

        private void Evaluations_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation)e.SelectedItem;
            ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
