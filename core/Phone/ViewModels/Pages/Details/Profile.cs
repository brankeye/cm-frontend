using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Profile : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Profiles ProfilesRealm { get; set; }

        public void Initialize(int profileLocalId)
        {
            ProfileId = profileLocalId;
        }

        public void OnAppearing()
        {
            ProfilesRealm = new Domain.Services.Realms.Profiles();
            ProfileModel = ProfilesRealm.Get(ProfileId);
        }

        private int ProfileId { get; set; }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profile; }
            set { this.SetProperty(ref _profile, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profile;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
