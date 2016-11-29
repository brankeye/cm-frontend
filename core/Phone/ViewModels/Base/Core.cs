using cm.frontend.core.Domain.Objects;
using cm.frontend.core.Phone.Services;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Base
{
    public class Core
    {
        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {

        }

        public Context GetContext()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");
            return currentContext;
        }

        public bool UserIsTeacher()
        {
            var currentUser = GetCurrentUser();
            var membersRealm = new Domain.Services.Realms.Members();
            var profile = currentUser.Profile;
            var member = membersRealm.Get(x => x.Profile == profile);
            return member.IsTeacher;
        }

        public Domain.Models.User GetCurrentUser()
        {
            var currentContext = GetContext();
            var usersRealm = new Domain.Services.Realms.Users();
            var username = currentContext.Username;
            var user = usersRealm.Get(x => x.Username == username);
            return user;
        }

        public Domain.Models.School GetCurrentSchool()
        {
            var currentContext = GetContext();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolName = currentContext.SchoolName;
            var school = schoolsRealm.Get(x => x.Name == schoolName);
            return school;
        }

        public Domain.Models.Member GetCurrentMember()
        {
            var currentUser = GetCurrentUser();
            var membersRealm = new Domain.Services.Realms.Members();
            var profile = currentUser.Profile;
            var member = membersRealm.Get(x => x.Profile == profile);
            return member;
        }

        public void SaveContext(Context context)
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            contextCache.Replace("Context", context);
        }

        public void SaveContext(string username, Domain.Objects.Token token, bool isAuthenticated)
        {
            var currentContext = GetContext();
            currentContext.Username = username;
            currentContext.AccessToken = token;
            currentContext.IsAuthenticated = isAuthenticated;

            SaveContext(currentContext);
        }

        public INavigation Navigation { get; set; }

        protected Services.Navigator Navigator => new Navigator();
    }
}
