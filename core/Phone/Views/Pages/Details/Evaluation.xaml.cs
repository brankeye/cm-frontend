using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            InitializeComponent();
            
            if (ViewModel.IsTeacher())
            {
                var editEvaluation = new ToolbarItem
                {
                    Text = "Edit",
                    Order = ToolbarItemOrder.Primary
                };
                editEvaluation.SetBinding(MenuItem.CommandProperty, new Binding("EditCommand"));

                var addSection = new ToolbarItem
                {
                    Text = "Add section",
                    Order = ToolbarItemOrder.Primary
                };
                addSection.SetBinding(MenuItem.CommandProperty, new Binding("AddSectionCommand"));

                ToolbarItems.Add(editEvaluation);
                ToolbarItems.Add(addSection);
            }
        }

        public void Initialize(int evalLocalId)
        {
            ViewModel.Initialize(evalLocalId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void Sections_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var section = (ViewModels.Controls.PrettyListViewItems.EvaluationSection) e.SelectedItem;
            ViewModel.EditSection(section.SectionModel.LocalId);
        }
    }
}
