using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class StudentEvaluations
    {
        private Base.PageController<ViewModels.Pages.Details.StudentEvaluations> PageController { get; }

        public StudentEvaluations()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.StudentEvaluations>(this);
            var header = (View) EvaluationsListView.Header;
            header.BindingContext = BindingContext;
            
            if (PageController.ViewModel.IsTeacher())
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
            PageController.ViewModel.Initialize(profileLocalId);
        }

        private void Evaluations_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation)e.SelectedItem;
            PageController.ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
