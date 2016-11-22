namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class School
    {
        public School()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
