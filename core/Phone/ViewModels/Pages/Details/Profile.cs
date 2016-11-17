using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Profile : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Profiles ProfilesRealm { get; set; }

        private void Initialize()
        {
            ProfilesRealm = new Domain.Services.Realms.Profiles();
            ProfileModel = ProfilesRealm.Get(1);
        }

        public void OnAppearing()
        {
            Initialize();
        }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profile; }
            set { this.SetProperty(ref _profile, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profile;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
