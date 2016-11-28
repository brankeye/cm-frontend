using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Login : ViewModels.Base.Core
    {
        private async void LoginOrRegister()
        {
            if (IsNewUser)
            {
                await HandleRegister();
            }
            else // is registered
            {
                await HandleLogin();
            }
        }

        private async Task HandleRegister()
        {
            var isRegistered = await RegisterAccount();
            if (isRegistered)
            {
                var token = await RequestToken();
                if (token != null)
                {
                    // navigate register ui path
                    SaveContext(Email, token, true);
                    await Navigator.PushProfileEditorPageAsync(Navigation);
                }
            }
        }

        private async Task HandleLogin()
        {
            var token = await RequestToken();
            if (token != null)
            {
                // save token and continue
                SaveContext(Email, token, true);

                var synchronizer = new Domain.Services.Sync.Synchronizer();
                await synchronizer.SyncAllAndWait();

                var profilesRealm = new Domain.Services.Realms.Profiles();
                var listProfs = profilesRealm.GetAll().ToList();

                var evalsRealm = new Domain.Services.Realms.Evaluations();
                var evsProfs = evalsRealm.GetAll().ToList();

                var membersRealme = new Domain.Services.Realms.Members();
                var listMembers = membersRealme.GetAll().ToList();

                var classesRealm = new Domain.Services.Realms.Classes();
                var classesProfs = classesRealm.GetAll().ToList();

                var schoolsRealm = new Domain.Services.Realms.Schools();
                var csProfs = schoolsRealm.GetAll().ToList();

                var usersRealm = new Domain.Services.Realms.Users();
                var us = usersRealm.GetAll().ToList();

                var membersRealm = new Domain.Services.Realms.Members();
                var profile = GetCurrentUser().Profile;
                var member = membersRealm.Get(x => x.Profile == profile);
                var currentContext = GetContext();
                currentContext.SchoolName = member.School.Name;
                SaveContext(currentContext);

                await Navigator.PushDashboardPageAsync(Navigation);
            }
        }

        private async Task<bool> RegisterAccount()
        {
            var accountService = new Domain.Services.Rest.Security.Account();
            var registerResult = await accountService.PostRegisterAsync(Email, Password);
            return registerResult.IsSuccessStatusCode;
        }

        private async Task<Domain.Objects.Token> RequestToken()
        {
            var tokenService = new Domain.Services.Rest.Security.Token();
            var token = await tokenService.PostAsync(Email, Password);
            return token;
        }

        public bool IsNewUser { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(LoginOrRegister));
        private ICommand _actionButtonCommand;
    }
}