using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Login
    {
        private async void LoginOrRegister()
        {
            var navigator = new Services.Navigator();
            if (NewUserIsToggled)
            {
                // navigate register ui path
                await navigator.PushProfileEditorPageAsync(Navigation);
            }
            else // is register
            {
                // navigate login ui path
                await navigator.PushDashboardPageAsync(Navigation);
            }
        }

        public bool NewUserIsToggled { get; set; }

        public INavigation Navigation { get; set; }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(LoginOrRegister));
        private ICommand _actionButtonCommand;
    }
}
