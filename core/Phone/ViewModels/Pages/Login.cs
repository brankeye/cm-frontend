using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Login : ViewModels.Base.Core
    {
        private async void LoginOrRegister()
        {
            if (NewUserIsToggled)
            {
                // navigate register ui path
                await Navigator.PushProfileEditorPageAsync(Navigation);
            }
            else // is register
            {
                // navigate login ui path
                await Navigator.PushDashboardPageAsync(Navigation);
            }
        }

        public bool NewUserIsToggled { get; set; }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(LoginOrRegister));
        private ICommand _actionButtonCommand;
    }
}
