using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class School : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Schools SchoolsRealm { get; set; }

        private void Initialize()
        {
            SchoolsRealm = new Domain.Services.Realms.Schools();
            SchoolModel = SchoolsRealm.Get(1);
        }

        public override void OnAppearing()
        {
            Initialize();
        }

        public Domain.Models.School SchoolModel
        {
            get { return _school; }
            set { this.SetProperty(ref _school, value, PropertyChanged); }
        }
        private Domain.Models.School _school;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
