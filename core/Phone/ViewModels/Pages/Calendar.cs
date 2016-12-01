using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Calendar : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; set; }

        private void Initialize()
        {
            
        }

        public override void OnAppearing()
        {
            Initialize();
        }

        public void LoadClassDates()
        {
            // get classes
            ClassesRealm = new Domain.Services.Realms.Classes();
            var currentSchool = GetCurrentSchool();
            var classes = ClassesRealm.GetAll(x => x.School == currentSchool).ToList();
            var classesContainer = new List<ViewModels.Controls.PrettyListViewItems.ClassDate>();

            var currentDate = SelectedDate;
            for (var i = 0; i < 7; i++)
            {
                // increase current date by one day
                currentDate = currentDate.AddDays(1);

                // loop through all classes
                foreach (var classModel in classes)
                {
                    // if class falls on the current date, add it to the list with the current date
                    if (classModel.Day == currentDate.DayOfWeek.ToString())
                    {
                        var classDate = currentDate.UtcDateTime.Date;
                        var canceledClassesRealm = new Domain.Services.Realms.CanceledClasses();
                        var canceledClass =
                            canceledClassesRealm.GetRealmResults()
                                .Where(x => x.Class == classModel)
                                .FirstOrDefault(x => x.Date == classDate);
                        var classIsCanceled = canceledClass != null;
                        classesContainer.Add(new ViewModels.Controls.PrettyListViewItems.ClassDate(classModel, currentDate, classIsCanceled));
                    }
                }
            }

            ClassesList.Clear();
            ClassesList.AddRange(classesContainer);
        }

        public async void ClassSelected(int classLocalId, DateTimeOffset date)
        {
            await Navigator.PushCalendarClassPageAsync(Navigation, classLocalId, date);
        }

        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value.UtcDateTime.Date;
                LoadClassDates();
            }
        }
        private DateTimeOffset _selectedDate;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.ClassDate> ClassesList
        {
            get { return _classes ?? (_classes = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.ClassDate>()); }
            set { this.SetProperty(ref _classes, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.ClassDate> _classes;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
