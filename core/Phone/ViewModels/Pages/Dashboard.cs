using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Dashboard : ViewModels.Base.Core
    {
        private async void Launch(string pageName)
        {
            switch (pageName)
            {
                case "Calendar":
                {
                    await Navigator.PushCalendarPageAsync(Navigation);
                    break;
                }
                case "Classes":
                {
                    await Navigator.PushClassesPageAsync(Navigation);
                    break;
                }
                case "Students":
                {
                    await Navigator.PushStudentsPageAsync(Navigation);
                    break;
                }
                case "Evaluations":
                {
                    await Navigator.PushEvaluationsPageAsync(Navigation, GetCurrentUser().Profile.LocalId);
                    break;
                }
                case "Profile":
                {
                    await Navigator.PushProfilePageAsync(Navigation, GetCurrentUser().Profile.LocalId);
                    break;
                }
                case "School":
                {
                    await Navigator.PushSchoolPageAsync(Navigation);
                    break;
                }
                case "Signout":
                {
                    var accountService = new Domain.Services.Rest.Security.Account();
                    var logoutResult = await accountService.PostLogoutAsync(GetContext().AccessToken.Access_Token);

                    if (logoutResult.IsSuccessStatusCode)
                    {
                        var synchronizer = new Domain.Services.Sync.Synchronizer();
                        await synchronizer.SyncAllAndWait();
                        SaveContext(null, null, false);
                        await Navigator.PopToRootAsync(Navigation);
                    }
                    break;
                }
            };
        }

        public bool IsTeacher()
        {
            return UserIsTeacher();
        }

        public ICommand LaunchCommand => _launchCommand ?? (_launchCommand = new Command<string>(Launch));
        private ICommand _launchCommand;
    }
}
