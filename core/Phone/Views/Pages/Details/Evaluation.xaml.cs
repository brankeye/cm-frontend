using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Evaluation
    {
        private Base.PageController<ViewModels.Pages.Details.Evaluation> PageController { get; }

        public Evaluation()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.Evaluation>(this);

            if (PageController.ViewModel.IsTeacher())
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
            PageController.ViewModel.Initialize(evalLocalId);
        }

        private void Sections_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var section = (ViewModels.Controls.PrettyListViewItems.EvaluationSection) e.SelectedItem;
            PageController.ViewModel.EditSection(section.SectionModel.LocalId);
        }
    }
}
