using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Models;
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
            if (await RegisterAccount())
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