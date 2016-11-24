using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Profile : ViewModels.Base.Core, INotifyPropertyChanged
    {
        public Profile()
        {
            ProfileModel = new Domain.Models.Profile();
        }

        private async void SaveProfile()
        {
            var currentContext = GetContext();

            // save profile
            var profilesRestService = new Domain.Services.Rest.Profiles();
            var httpResponse = await profilesRestService.PostAsync(ProfileModel, currentContext.AccessToken.Access_Token);
            var profile = await profilesRestService.ParseResponseItem(httpResponse);

            var profilesRealm = new Domain.Services.Realms.Profiles();
            var profileLocalId = 0;
            await profilesRealm.WriteAsync(realm =>
            {
                realm.Manage(profile);
                profileLocalId = profile.LocalId;
            });
            var list = profilesRealm.GetAll().ToList();

            // save user
            var usersRealm = new Domain.Services.Realms.Users();
            var userLocalId = 0;
            await usersRealm.WriteAsync(realm =>
            {
                var userModel = realm.CreateObject();
                userLocalId = userModel.LocalId;
                var prof = profilesRealm.Get(profileLocalId);
                userModel.Profile = prof;
                userModel.ProfileId = prof.Id;
                userModel.Username = GetContext().Username;
            });

            var user = usersRealm.Get(userLocalId);
            var usersRestService = new Domain.Services.Rest.Users();
            httpResponse = await usersRestService.PostAsync(user, currentContext.AccessToken.Access_Token);
            var userItem = await profilesRestService.ParseResponseItem(httpResponse);
            var userId = userItem.Id;

            await usersRealm.WriteAsync(realm =>
            {
                var userModel = realm.Get(userLocalId);
                userModel.Id = userId;
            });

            // navigate to manage page to either join or create a club
            await Navigator.PushManagePageAsync(Navigation);
        }

        private async void Cancel()
        {
            await Navigator.PopAsync(Navigation);
        }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profileModel; }
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
