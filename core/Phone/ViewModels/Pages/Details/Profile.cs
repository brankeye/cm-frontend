using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Profile : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Profiles ProfilesRealm { get; set; }

        public Profile()
        {
            // if no profile id is set, use the current users id
            ProfileId = GetCurrentUser().Profile.LocalId;
        }

        public void Initialize(int profileLocalId)
        {
            ProfileId = profileLocalId;
        }

        public override void OnAppearing()
        {
            ProfilesRealm = new Domain.Services.Realms.Profiles();
            ProfileModel = ProfilesRealm.Get(ProfileId);
        }

        private async void EditProfile()
        {
            await Navigator.PushProfileEditorPageAsync(Navigation, false);
        }

        private int ProfileId { get; set; }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profile; }
            set { this.SetProperty(ref _profile, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profile;

        public ICommand EditProfileCommand => _editProfileCommand ?? (_editProfileCommand = new Command(EditProfile));
        private ICommand _editProfileCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
