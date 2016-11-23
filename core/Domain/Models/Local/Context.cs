using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Domain.Models.Local
{
    public class Context : INotifyPropertyChanged
    {
        public Models.User CurrentUser
        {
            get { return _currentUser; }
            set { this.SetProperty(ref _currentUser, value, PropertyChanged); }
        }
        private Models.User _currentUser;

        public bool IsAuthenticated { get; set; }

        public string AccessToken { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
