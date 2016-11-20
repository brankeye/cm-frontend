using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Evaluation : ContentPage
    {
        public ViewModels.Pages.Details.Evaluation ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.Evaluation);
        private ViewModels.Pages.Details.Evaluation _vm;

        public Evaluation()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.Evaluation();
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
