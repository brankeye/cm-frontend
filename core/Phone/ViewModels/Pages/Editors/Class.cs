using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Models;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Class : INotifyPropertyChanged
    {
        public Class()
        {
            Days = new List<string>()
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            IsNewClass = false;
        }

        public void Initialize(int classLocalId)
        {
            var classesRealm = new Domain.Services.Realms.Classes();
            var existingClass = classesRealm.Get(classLocalId);
            ClassName = existingClass.Name;
            DaysIndex = Days.IndexOf(existingClass.Day);
            StartTime = new TimeSpan(existingClass.StartTime.Hour, existingClass.StartTime.Minutes, 0);
            EndTime = new TimeSpan(existingClass.EndTime.Hour, existingClass.EndTime.Minutes, 0);
            IsNewClass = true;
            ClassLocalId = classLocalId;
        }

        private async void SaveClass()
        {
            var classesRealm = new Domain.Services.Realms.Classes();

            if (IsNewClass)
            {
                await classesRealm.WriteAsync(realm =>
                {
                    var newClass = realm.CreateObject();
                    newClass.Name = ClassName;
                    newClass.Day = Days[DaysIndex];
                    newClass.StartTime = new Time(StartTime.Hours, StartTime.Minutes, 0);
                    newClass.EndTime = new Time(EndTime.Hours, EndTime.Minutes, 0);
                });
            }
            else
            {
                await classesRealm.WriteAsync(realm =>
                {
                    var existingClass = realm.Get(ClassLocalId);
                    existingClass.Name = ClassName;
                    existingClass.Day = Days[DaysIndex];
                    existingClass.StartTime = new Time(StartTime.Hours, StartTime.Minutes, 0);
                    existingClass.EndTime = new Time(EndTime.Hours, EndTime.Minutes, 0);
                });
            }
            

            var navigator = new Services.Navigator();
            await navigator.PopAsync(Navigation);
        }

        private async void Cancel()
        {
            var navigator = new Services.Navigator();
            await navigator.PopAsync(Navigation);
        }

        private bool IsNewClass { get; set; }

        private int ClassLocalId { get; set; }

        public string ClassName
        {
            get { return _className; }
            set { this.SetProperty(ref _className, value, PropertyChanged); }
        }
        private string _className;

        public List<string> Days
        {
            get { return _days ?? (_days = new List<string>()); }
            set { this.SetProperty(ref _days, value, PropertyChanged); }
        }
        private List<string> _days;

        public int DaysIndex
        {
            get { return _daysIndex; }
            set { this.SetProperty(ref _daysIndex, value, PropertyChanged); }
        }
        private int _daysIndex;

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { this.SetProperty(ref _startTime, value, PropertyChanged); }
        }
        private TimeSpan _startTime;

        public TimeSpan EndTime
        {
            get { return _endTime; }
            set { this.SetProperty(ref _endTime, value, PropertyChanged); }
        }
        private TimeSpan _endTime;

        public INavigation Navigation { get; set; }

        public ICommand SaveClassCommand => _saveClassCommand ?? (_saveClassCommand = new Command(SaveClass));
        private ICommand _saveClassCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
