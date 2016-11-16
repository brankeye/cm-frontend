using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Manage
    {
        private async void Join()
        {
            var navigator = new Services.Navigator();
            // navigate to dashboard page for students
            await navigator.PushDashboardPageAsync(Navigation);
        }

        private async void Create()
        {
            var navigator = new Services.Navigator();
            // navigate to dashboard page for teachers
            await navigator.PushDashboardPageAsync(Navigation);
        }

        public INavigation Navigation { get; set; }

        public ICommand JoinCommand => _joinCommand ?? (_joinCommand = new Command(Join));
        private ICommand _joinCommand;

        public ICommand CreateCommand => _createCommand ?? (_createCommand = new Command(Create));
        private ICommand _createCommand;
    }
}
