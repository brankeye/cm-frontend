using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Students : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Students StudentsRealm { get; set; }

        private void Initialize()
        {
            StudentsRealm = new Domain.Services.Realms.Students();
            var students = StudentsRealm.GetAll().ToList();
            var studentsContainer = new List<ViewModels.Controls.PrettyListViewItems.Student>();

            foreach (var studentsModel in students)
            {
                if (studentsModel.School.LocalId == 1)
                {
                    studentsContainer.Add(new ViewModels.Controls.PrettyListViewItems.Student(studentsModel.Student));
                }
            }

            StudentsList.AddRange(studentsContainer);
        }

        public void OnAppearing()
        {
            Initialize();
        }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student> StudentsList
        {
            get { return _students ?? (_students = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student>()); }
            set { this.SetProperty(ref _students, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Student> _students;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
