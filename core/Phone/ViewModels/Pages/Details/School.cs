using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class School : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Members MembersRealm { get; } = new Members();

        public sealed override void RefreshData()
        {
            var currentProfile = GetCurrentUser().Profile;
            SchoolModel = MembersRealm.Get(x => x.Profile == currentProfile).School;
            Teacher = MembersRealm.Get(x => x.IsTeacher).Profile;
        }

        private async void EditSchool()
        {
            await Navigator.PushSchoolEditorPageAsync(Navigation);
        }

        public Domain.Models.School SchoolModel
        {
            get { return _school; }
            set { this.SetProperty(ref _school, value, PropertyChanged, true); }
        }
        private Domain.Models.School _school;

        public Domain.Models.Profile Teacher
        {
            get { return _teacher; }
            set { this.SetProperty(ref _teacher, value, PropertyChanged, true); }
        }
        private Domain.Models.Profile _teacher;

        public ICommand EditSchoolCommand => _editSchoolCommand ?? (_editSchoolCommand = new Command(EditSchool));
        private ICommand _editSchoolCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
