using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Profile : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Profiles ProfilesRealm { get; } = new Profiles();

        public Profile()
        {
            // if no profile id is set, use the current users id
            ProfileId = GetCurrentUser().Profile.LocalId;
        }

        public void Initialize(int profileLocalId)
        {
            ProfileId = profileLocalId;
        }

        public sealed override void RefreshData()
        {
#if DEBUG_UI
            ProfileModel = new Domain.Models.Profile
            {
                FirstName = "Brandon",
                LastName = "Keyes",
                Email = "brankeye@gmail.com",
                PhoneNumber = "6137095799",
                StartDate = DateTimeOffset.Now,
                Level = "Blue"
            };
#else
            ProfileModel = ProfilesRealm.Get(ProfileId);
#endif
        }

        private async void EditProfile()
        {
            await Navigator.PushProfileEditorPageAsync(Navigation, false);
        }

        public int ProfileId { get; set; }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profile; }
            set { this.SetProperty(ref _profile, value, PropertyChanged, true); }
        }
        private Domain.Models.Profile _profile;

        public ICommand EditProfileCommand => _editProfileCommand ?? (_editProfileCommand = new Command(EditProfile));
        private ICommand _editProfileCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
