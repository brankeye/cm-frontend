using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            InitializeComponent();
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
