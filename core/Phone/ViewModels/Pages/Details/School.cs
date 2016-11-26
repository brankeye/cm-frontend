using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class School : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Members MembersRealm { get; set; }

        private void Initialize()
        {
            MembersRealm = new Domain.Services.Realms.Members();
            var currentProfile = GetCurrentUser().Profile;
            SchoolModel = MembersRealm.Get(x => x.Profile == currentProfile).School;
            var list = MembersRealm.GetAll();
            //var membersRestService = new Domain.Services.Rest.Members();
            //var members = await membersRestService.GetAsync(GetContext().AccessToken.Access_Token);
            //await MembersRealm.WriteAsync(realm =>
            //{
            //    realm.ManageAll(members);
            //});
            Teacher = MembersRealm.Get(x => x.IsTeacher).Profile;
        }

        public override void OnAppearing()
        {
            Initialize();
        }

        public Domain.Models.School SchoolModel
        {
            get { return _school; }
            set { this.SetProperty(ref _school, value, PropertyChanged); }
        }
        private Domain.Models.School _school;

        public Domain.Models.Profile Teacher
        {
            get { return _teacher; }
            set { this.SetProperty(ref _teacher, value, PropertyChanged); }
        }
        private Domain.Models.Profile _teacher;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
