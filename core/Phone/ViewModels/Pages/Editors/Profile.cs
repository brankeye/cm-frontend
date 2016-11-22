using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Profile : ViewModels.Base.Core
    {
        private async void FinishEditing()
        {
            // navigate to manage page to either join or create a club
            await Navigator.PushManagePageAsync(Navigation);
        }

        public ICommand ActionButtonCommand => _actionButtonCommand ?? (_actionButtonCommand = new Command(FinishEditing));
        private ICommand _actionButtonCommand;
    }
}
