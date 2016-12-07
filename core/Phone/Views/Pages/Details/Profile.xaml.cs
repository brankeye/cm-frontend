using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Profile
    {
        private Base.PageController<ViewModels.Pages.Details.Profile> PageController { get; }

        public Profile()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.Profile>(this);
        }

        public void Initialize(int profileLocalId)
        {
            PageController.ViewModel.Initialize(profileLocalId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var currentProfileLocalId = PageController.ViewModel.GetCurrentUser().Profile.LocalId;
            if (ToolbarItems.Count > 0) return;
            if (PageController.ViewModel.ProfileModel.LocalId == currentProfileLocalId)
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
