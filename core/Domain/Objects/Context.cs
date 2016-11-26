using System.ComponentModel;

namespace cm.frontend.core.Domain.Objects
{
    public class Context : INotifyPropertyChanged
    {
        public string Username { get; set; }

        public string SchoolName { get; set; }

        public bool IsAuthenticated { get; set; }

        public Domain.Objects.Token AccessToken { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
