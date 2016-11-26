using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class School : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private async void SaveSchool()
        {
            var currentContext = GetContext();

            // post school
            var postedSchool = await PostSchool(currentContext.AccessToken.Access_Token);

            // post member
            var profile = GetCurrentUser().Profile;
            var member = new Domain.Models.Member
            {
                ProfileId = profile.Id,
                SchoolId = postedSchool.Id,
                IsTeacher = IsManaging
            };
            var postedMember = await PostMember(currentContext.AccessToken.Access_Token, member);
            
            // save school
            var savedSchool = await SaveSchool(postedSchool);

            // save member
            var savedMember = await SaveMember(postedMember, profile, savedSchool);

            // navigate to dashboard
            await Navigator.PushDashboardPageAsync(Navigation);
        }

        private async Task<Domain.Models.School> PostSchool(string accessToken)
        {
            var schoolsRestService = new Domain.Services.Rest.Schools();
            var httpResponse = await schoolsRestService.PostAsync(SchoolModel, accessToken);
            var school = await schoolsRestService.ParseResponseItem(httpResponse);
            return school;
        }

        private async Task<Domain.Models.Member> PostMember(string accessToken, Domain.Models.Member memberModel)
        {
            var membersRestService = new Domain.Services.Rest.Members();
            var httpResponse = await membersRestService.PostAsync(memberModel, accessToken);
            var member = await membersRestService.ParseResponseItem(httpResponse);
            return member;
        }

        private async Task<Domain.Models.School> SaveSchool(Domain.Models.School school)
        {
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolLocalId = 0;
            await schoolsRealm.WriteAsync(realm =>
            {
                realm.Manage(school);
                schoolLocalId = school.LocalId;
                school.Synced = true;
            });
            var savedProfile = schoolsRealm.Get(schoolLocalId);
            return savedProfile;
        }

        // user => member // profile => school
        private async Task<Domain.Models.Member> SaveMember(Domain.Models.Member memberModel, Domain.Models.Profile profileModel, Domain.Models.School schoolModel)
        {
            var membersRealm = new Domain.Services.Realms.Members();
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var memberLocalId = 0;
            var profileLocalId = profileModel.LocalId;
            var schoolLocalId = schoolModel.LocalId;
            await membersRealm.WriteAsync(realm =>
            {
                realm.Manage(memberModel);
                memberModel.Profile = profilesRealm.Get(profileLocalId);
                memberModel.ProfileId = memberModel.Profile.Id;
                memberModel.School = schoolsRealm.Get(schoolLocalId);
                memberModel.SchoolId = memberModel.School.Id;
                memberLocalId = memberModel.LocalId;
                memberModel.Synced = true;
            });
            var savedMember = membersRealm.Get(memberLocalId);
            return savedMember;
        }

        private async void Cancel()
        {
            await Navigator.PopAsync(Navigation);
        }

        public bool IsManaging { get; set; }

        public Domain.Models.School SchoolModel
        {
            get { return _schoolModel ?? (_schoolModel = new Domain.Models.School()); }
            set { this.SetProperty(ref _schoolModel, value, PropertyChanged); }
        }
        private Domain.Models.School _schoolModel;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveSchool));
        private ICommand _saveCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
