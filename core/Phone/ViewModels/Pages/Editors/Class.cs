using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Class : ViewModels.Base.Core, INotifyPropertyChanged
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
            IsNewClass = true;
        }

        public void Initialize(int classLocalId)
        {
            var classesRealm = new Domain.Services.Realms.Classes();
            var existingClass = classesRealm.Get(classLocalId);
            ClassName = existingClass.Name;
            DaysIndex = Days.IndexOf(existingClass.Day);
            StartTime = new TimeSpan(existingClass.StartTime.Hour, existingClass.StartTime.Minute, 0);
            EndTime = new TimeSpan(existingClass.EndTime.Hour, existingClass.EndTime.Minute, 0);
            IsNewClass = false;
            ClassLocalId = classLocalId;
        }

        private async void SaveClass()
        {
            var classesRealm = new Domain.Services.Realms.Classes();

            await classesRealm.WriteAsync(realm =>
            {
                var classModel = IsNewClass ? realm.CreateObject() : realm.Get(ClassLocalId);
                classModel.Name = ClassName;
                classModel.Day = Days[DaysIndex];
                classModel.StartTime = new DateTimeOffset(1, 1, 1, StartTime.Hours, StartTime.Minutes, StartTime.Seconds, TimeSpan.Zero);
                classModel.EndTime = new DateTimeOffset(1, 1, 1, EndTime.Hours, EndTime.Minutes, EndTime.Seconds, TimeSpan.Zero);
                classModel.School = GetCurrentSchool();
                classModel.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            synchronizer.SyncPostsAndContinue();

            await Navigator.PopAsync(Navigation);
        }

        private async void Cancel()
        {
            await Navigator.PopAsync(Navigation);
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

        public ICommand SaveClassCommand => _saveClassCommand ?? (_saveClassCommand = new Command(SaveClass));
        private ICommand _saveClassCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
