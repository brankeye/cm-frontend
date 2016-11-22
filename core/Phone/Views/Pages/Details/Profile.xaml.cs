namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Profile
    {
        public Profile()
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
    }
}
