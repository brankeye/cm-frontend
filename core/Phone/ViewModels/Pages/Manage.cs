using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Manage : ViewModels.Base.Core
    {
        private async void Join()
        {
            // navigate to dashboard page for students
            await Navigator.PushDashboardPageAsync(Navigation);
        }

        private async void Create()
        {
            // navigate to dashboard page for teachers
            await Navigator.PushDashboardPageAsync(Navigation);
        }

        public ICommand JoinCommand => _joinCommand ?? (_joinCommand = new Command(Join));
        private ICommand _joinCommand;

        public ICommand CreateCommand => _createCommand ?? (_createCommand = new Command(Create));
        private ICommand _createCommand;
    }
}
