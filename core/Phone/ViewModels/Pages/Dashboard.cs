using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Dashboard
    {
        private async void Launch(string pageName)
        {
            var navigator = new Services.Navigator();

            switch (pageName)
            {
                case "Classes":
                {
                    await navigator.PushClassesPageAsync(Navigation);
                    break;
                }
                case "Students":
                {
                    await navigator.PushStudentsPageAsync(Navigation);
                    break;
                }
                case "Evaluations":
                {
                    await navigator.PushEvaluationsPageAsync(Navigation);
                    break;
                }
                case "Profile":
                {
                    await navigator.PushProfilePageAsync(Navigation);
                    break;
                }
                case "School":
                {
                    await navigator.PushSchoolPageAsync(Navigation);
                    break;
                }
                case "Signout":
                {
                    // TODO: Handle user signout
                    await navigator.PushLoginPageAsync(Navigation);
                    break;
                }
                default:
                    break;
            };
        }

        public INavigation Navigation { get; set; }

        public ICommand LaunchCommand => _launchCommand ?? (_launchCommand = new Command<string>(Launch));
        private ICommand _launchCommand;
    }
}
