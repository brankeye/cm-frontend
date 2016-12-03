using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Students : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Members MembersRealm { get; } = new Members();

        private void Initialize()
        {
            var currentSchool = GetCurrentSchool();
            var students = MembersRealm.GetAll(x => x.School == currentSchool).ToList();
            var studentsContainer = new List<ViewModels.Controls.PrettyListViewItems.Student>();

            var currentProfile = GetCurrentUser().Profile;
            var currentMember = MembersRealm.Get(x => x.Profile == currentProfile);
            students.Remove(currentMember);

            foreach (var studentsModel in students)
            {
                studentsContainer.Add(new ViewModels.Controls.PrettyListViewItems.Student(studentsModel.Profile));
            }
            StudentsList.Clear();
            StudentsList.AddRange(studentsContainer);
        }

        public override void OnAppearing()
        {
            Initialize();
        }

        public async void StudentSelected(int studentLocalId)
        {
            if (UserIsTeacher())
            {
                await Navigator.PushStudentEvaluationsPageAsync(Navigation, studentLocalId);
            }
            else
            {
                await Navigator.PushProfilePageAsync(Navigation, studentLocalId);
            }
        }

        private void AddStudent()
        {
            //await Navigator.PushProfilePageAsync(Navigation, studentLocalId);
        }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student> StudentsList
        {
            get { return _students ?? (_students = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student>()); }
            set { this.SetProperty(ref _students, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student> _students;

        public ICommand AddStudentCommand => _addStudentCommand ?? (_addStudentCommand = new Command(AddStudent));
        private ICommand _addStudentCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
