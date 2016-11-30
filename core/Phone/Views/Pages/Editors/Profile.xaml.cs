namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class Profile
    {
        public Profile()
        {
            InitializeComponent();
        }

        public void Initialize(bool isEditingNewProfile)
        {
            ViewModel.Initialize(isEditingNewProfile);
        }
    }
}
