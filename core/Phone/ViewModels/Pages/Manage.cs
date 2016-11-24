using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Manage : ViewModels.Base.Core
    {
        private async void Join()
        {
            var currentContext = GetContext();

            // get school by name
            var schoolsRestService = new Domain.Services.Rest.Schools();
            var school = await schoolsRestService.GetAsync(SchoolName, currentContext.AccessToken.Access_Token);

            if (school == null) return;

            // get current profile
            var profile = GetCurrentUser().Profile;

            // post member
            var member = await PostMember(school, profile);

            // save member
            await SaveMember(member, school, profile);

            // navigate to dashboard page for students
            await Navigator.PushDashboardPageAsync(Navigation);
        }

        private async Task<Domain.Models.Member> PostMember(Domain.Models.School school, Domain.Models.Profile profile)
        {
            var membersRestService = new Domain.Services.Rest.Members();
            var member = new Domain.Models.Member
            {
                ProfileId = profile.Id,
                SchoolId = school.Id
            };
            var response = await membersRestService.PostAsync(member, GetContext().AccessToken.Access_Token);
            var result = await membersRestService.ParseResponseItem(response);
            return result;
        }

        private async Task<Domain.Models.Member> SaveMember(Domain.Models.Member memberModel, Domain.Models.School school, Domain.Models.Profile profile)
        {
            var membersRealm = new Domain.Services.Realms.Members();
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var profileLocalId = profile.LocalId;
            var schoolLocalId = school.LocalId;
            var memberLocalId = 0;
            await membersRealm.WriteAsync(realm =>
            {
                realm.Manage(memberModel);
                memberLocalId = memberModel.LocalId;
                memberModel.Profile = profilesRealm.Get(profileLocalId);
                memberModel.School = schoolsRealm.Get(schoolLocalId);
            });
            var member = membersRealm.Get(memberLocalId);
            return member;
        }

        private async void Create()
        {
            // post profile w/ IsTeacher flag set to true
            var currentUser = GetCurrentUser();
            var profileLocalId = currentUser.Profile.LocalId;
            var profilesRealm = new Domain.Services.Realms.Profiles();
            await profilesRealm.WriteAsync(realm =>
            {
                var profile = realm.Get(profileLocalId);
                profile.IsTeacher = true;
            });
            var profileModel = profilesRealm.Get(profileLocalId);
            var profilesRestService = new Domain.Services.Rest.Profiles();
            var postedProfile = await profilesRestService.PostAsync(profileModel, GetContext().AccessToken.Access_Token);

            await Navigator.PushSchoolEditorPageAsync(Navigation, SchoolName);
        }

        public string SchoolName { get; set; }

        public ICommand JoinCommand => _joinCommand ?? (_joinCommand = new Command(Join));
        private ICommand _joinCommand;

        public ICommand CreateCommand => _createCommand ?? (_createCommand = new Command(Create));
        private ICommand _createCommand;
    }
}
