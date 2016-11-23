using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Login : ViewModels.Base.Core
    {
        private async void LoginOrRegister()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");

            if (NewUserIsToggled)
            {
                var accountService = new Domain.Services.Rest.Security.Account();
                var registerResult = await accountService.PostRegisterAsync(Email, Password);

                if (registerResult.IsSuccessStatusCode)
                {
                    var tokenService = new Domain.Services.Rest.Security.Token();
                    var token = await tokenService.PostAsync(Email, Password);

                    if (token != null)
                    {
                        // save the token
                        currentContext.AccessToken = token.Access_Token;
                        currentContext.IsAuthenticated = true;
                        contextCache.Replace("Context", currentContext);

                        // navigate register ui path
                        await Navigator.PushProfileEditorPageAsync(Navigation);
                    }
                }
            }
            else // is registered
            {
                var tokenService = new Domain.Services.Rest.Security.Token();
                var token = await tokenService.PostAsync(Email, Password);

                if (token != null)
                {
                    // save token and continue
                    // save the token
                    currentContext.AccessToken = token.Access_Token;
                    currentContext.IsAuthenticated = true;
                    contextCache.Replace("Context", currentContext);

                    await Navigator.PushDashboardPageAsync(Navigation);
                }
            }
        }

        public bool NewUserIsToggled { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(LoginOrRegister));
        private ICommand _actionButtonCommand;
    }
}