using Xamarin.Forms;

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

            var currentProfileLocalId = ViewModel.GetCurrentUser().Profile.LocalId;
            if (ToolbarItems.Count > 0) return;
            if (ViewModel.ProfileModel.LocalId == currentProfileLocalId)
            {
                var profileEditButton = new ToolbarItem
                {
                    Text = "Edit",
                    Order = ToolbarItemOrder.Primary
                };
                profileEditButton.SetBinding(MenuItem.CommandProperty, new Binding("EditProfileCommand"));
                ToolbarItems.Add(profileEditButton);
            }
        }
    }
}
