﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Login : ViewModels.Base.Core
    {
        private async void LoginOrRegister()
        {
            if (!Validate()) return;

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
                    SaveContext(Email, token, true, null);
                    await Navigator.PushProfileEditorPageAsync(Navigation, true);
                }
            }
            else
            {
                DisplayAlert("Register failed", "Either existing user or server is down.");
            }
        }

        private async Task HandleLogin()
        {
            var token = await RequestToken();
            if (token != null)
            {
                // save token and continue
                SaveContext(Email, token, true, null);

                var currentContext = GetContext();

                var synchronizer = new Domain.Services.Sync.Synchronizer();
                await synchronizer.SyncAllAndWait();

                var membersRealm = new Domain.Services.Realms.Members();
                var profile = GetCurrentUser().Profile;
                var member = membersRealm.Get(x => x.Profile == profile);
                currentContext.SchoolName = member.School.Name;
                SaveContext(currentContext);

                Application.Current.Properties["Context"] = JsonConvert.SerializeObject(currentContext);

                App.LaunchMasterDetailPage?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                DisplayAlert("Login failed", "Either wrong credentials or no such user.");
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
            if (token == null) return null;
            if (token.Access_Token == null)
            {
                token = null;
            }
            return token;
        }

        private bool Validate()
        {
            if (!IsEmailValid)
            {
                DisplayAlert("Invalid email", "Must be formatted correctly, as in 'abc123@gmail.com', for example.");
                return false;
            }
            
            if (!IsPasswordValid)
            {
                DisplayAlert("Invalid password", "Minimum 8 characters, 1 upper case, 1 number, and 1 special character.");
                return false;
            }

            return true;
        }

        public bool IsNewUser { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsEmailValid { get; set; }

        public bool IsPasswordValid { get; set; }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(LoginOrRegister));
        private ICommand _actionButtonCommand;
    }
}