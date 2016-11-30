using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Profile : ViewModels.Base.Core, INotifyPropertyChanged
    {
        public Profile()
        {
            IsEditingNewProfile = false;
        }

        public void Initialize(bool isEditingNewProfile)
        {
            IsEditingNewProfile = isEditingNewProfile;
        }

        private async void SaveProfile()
        {
            if (IsEditingNewProfile)
            {
                await SaveNewProfile();
            }
            else
            {
                await SaveExistingProfile();
            }
        }

        private async Task SaveExistingProfile()
        {
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var profileLocalId = GetCurrentUser().Profile.LocalId;
            await profilesRealm.WriteAsync(realm =>
            {
                var profile = realm.Get(profileLocalId);
                var mapper = new Domain.Utilities.PropertyMapper<Domain.Models.Profile>();
                mapper.Map(ProfileModel, profile);
                profile.Synced = false;
            });
            var synchronizer = new Domain.Services.Sync.Synchronizer();
            await synchronizer.SyncAllAndWait();
        }

        private async Task SaveNewProfile()
        {
            var currentContext = GetContext();

            // post profile
            var postedProfile = await PostProfile(currentContext.AccessToken.Access_Token);
            
            // post user
            var user = new Domain.Models.User
            {
                Username = currentContext.Username,
                ProfileId = postedProfile.Id,
            };
            var postedUser = await PostUser(currentContext.AccessToken.Access_Token, user);

            // save profile
            var savedProfile = await SaveProfile(postedProfile);
            var savedUser = await SaveUser(postedUser, savedProfile);

            // navigate to manage page to either join or create a club
            await Navigator.PushManagePageAsync(Navigation);
        }

        public override void OnAppearing()
        {
            if (!IsEditingNewProfile)
            {
                var mapper = new Domain.Utilities.PropertyMapper<Domain.Models.Profile>();
                var profile = GetCurrentUser().Profile;
                mapper.Map(profile, ProfileModel);
            }
        }

        private bool IsEditingNewProfile { get; set; }

        private async Task<Domain.Models.Profile> PostProfile(string accessToken)
        {
            var profilesRestService = new Domain.Services.Rest.Profiles();
            var httpResponse = await profilesRestService.PostAsync(ProfileModel, accessToken);
            var profile = await profilesRestService.ParseResponseItem(httpResponse);
            return profile;
        }

        private async Task<Domain.Models.User> PostUser(string accessToken, Domain.Models.User userModel)
        {
            var usersRestService = new Domain.Services.Rest.Users();
            var httpResponse = await usersRestService.PostAsync(userModel, accessToken);
            var user = await usersRestService.ParseResponseItem(httpResponse);
            return user;
        }

        private async Task<Domain.Models.Profile> SaveProfile(Domain.Models.Profile profile)
        {
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var profileLocalId = 0;
            await profilesRealm.WriteAsync(realm =>
            {
                realm.Manage(profile);
                profileLocalId = profile.LocalId;
                profile.Synced = true;
            });

            var savedProfile = profilesRealm.Get(profileLocalId);
            return savedProfile;
        }

        private async Task<Domain.Models.User> SaveUser(Domain.Models.User userModel, Domain.Models.Profile profileModel)
        {
            var usersRealm = new Domain.Services.Realms.Users();
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var userLocalId = 0;
            var profileLocalId = profileModel.LocalId;
            await usersRealm.WriteAsync(realm =>
            {
                realm.Manage(userModel);
                userModel.Profile = profilesRealm.Get(profileLocalId);
                userModel.ProfileId = userModel.Profile.Id;
                userLocalId = userModel.LocalId;
                userModel.Synced = true;
            });
            var savedUser = usersRealm.Get(userLocalId);
            return savedUser;
        }

        private async void Cancel()
        {
            await Navigator.PopAsync(Navigation);
        }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profileModel ?? (_profileModel = new Domain.Models.Profile()); }
            set { this.SetProperty(ref _profileModel, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profileModel;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveProfile));
        private ICommand _saveCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
