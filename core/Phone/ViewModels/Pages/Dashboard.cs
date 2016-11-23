using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Dashboard : ViewModels.Base.Core
    {
        private async void Launch(string pageName)
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");

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
                    await Navigator.PushEvaluationsPageAsync(Navigation, currentContext.CurrentUser.Profile.LocalId);
                    break;
                }
                case "Profile":
                {
                    await Navigator.PushProfilePageAsync(Navigation, currentContext.CurrentUser.Profile.LocalId);
                    break;
                }
                case "School":
                {
                    await Navigator.PushSchoolPageAsync(Navigation);
                    break;
                }
                case "Signout":
                {
                    // TODO: Handle user signout
                    await Navigator.PushLoginPageAsync(Navigation);
                    break;
                }
                default:
                    break;
            };
        }

        public ICommand LaunchCommand => _launchCommand ?? (_launchCommand = new Command<string>(Launch));
        private ICommand _launchCommand;
    }
}
