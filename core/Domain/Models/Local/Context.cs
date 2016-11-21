using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Domain.Models.Local
{
    public class Context : INotifyPropertyChanged
    {
        public Models.Account CurrentAccount
        {
            get { return _currentAccount; }
            set { this.SetProperty(ref _currentAccount, value, PropertyChanged); }
        }
        private Models.Account _currentAccount;

        public bool IsTeacher { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
