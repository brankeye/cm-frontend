using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
