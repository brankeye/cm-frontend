using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class CalendarClass : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; } = new Domain.Services.Realms.Classes();

        private Domain.Services.Realms.AttendanceRecords AttendanceRealm { get; } = new AttendanceRecords();

        public CalendarClass()
        {
            CanceledOptions = new List<string>()
            {
                "Yes",
                "No"
            };
            CanceledIndex = 1;

            IsCanceledViewVisible = UserIsTeacher();
        }

        public void Initialize(int classLocalId, DateTimeOffset date)
        {
            ClassLocalId = classLocalId;
            Date = date.UtcDateTime.Date;
        }

        public override void RefreshData()
        {
            ClassModel = ClassesRealm.Get(ClassLocalId);

            var currentProfile = GetCurrentUser().Profile;
            AttendanceModel = AttendanceRealm.GetRealmResults()
                                              .Where(x => x.Date == Date)
                                              .FirstOrDefault(x => x.Profile == currentProfile);

            var canceledRealm = new Domain.Services.Realms.CanceledClasses();
            CanceledModel = canceledRealm.GetRealmResults().Where(x => x.Class == ClassModel).FirstOrDefault(x => x.Date == Date);
            if (CanceledModel != null) IsCanceled = CanceledModel.IsCanceled;

            GetAttendants();
        }

        private void GetAttendants()
        {
            var attendants = AttendanceRealm.GetAll(x => x.Date == Date).ToList();
            var attList = attendants.Select(attendant => new ViewModels.Controls.PrettyListViewItems.AttendingClass(attendant)).ToList();

            AttendingList.Clear();
            AttendingList.AddRange(attList);
        }

        public async void HandleCanceledClass()
        {
            var canceledRealm = new Domain.Services.Realms.CanceledClasses();
            var recordExists = CanceledModel != null;
            var canceledLocalId = 0;
            if(recordExists) canceledLocalId = CanceledModel.LocalId;
            await canceledRealm.WriteAsync(realm =>
            {
                var record = recordExists ? realm.Get(canceledLocalId) : realm.CreateObject();
                record.Date = Date.UtcDateTime.Date;
                record.IsCanceled = IsCanceled;
                realm.Manage(record);
                record.Class = ClassModel;
                record.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            synchronizer.SyncPostsAndContinue();
        }

        public async void HandleAttendance(string isAttendingStr)
        {
            var isAttending = bool.Parse(isAttendingStr);
            var currentProfile = GetCurrentUser().Profile;

            // the user has decided to change their attendance, so we first have to check if
            // a record of their attendance exists for this particular class
            AttendanceModel = AttendanceRealm.GetRealmResults()
                                              .Where(x => x.Date == Date)
                                              .FirstOrDefault(x => x.Profile == currentProfile);
            
            var recordExists = AttendanceModel != null;
            var attendanceLocalId = 0;
            if (recordExists) attendanceLocalId = AttendanceModel.LocalId;
            await AttendanceRealm.WriteAsync(realm =>
            {
                var attendanceRecord = recordExists ? realm.Get(attendanceLocalId) : realm.CreateObject();
                attendanceRecord.Class = ClassModel;
                attendanceRecord.Date = Date.UtcDateTime.Date;
                attendanceRecord.Profile = currentProfile;
                attendanceRecord.IsAttending = isAttending;
                attendanceRecord.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            synchronizer.SyncPostsAndContinue();

            GetAttendants();
        }

        private int ClassLocalId { get; set; }

        public Domain.Models.Class ClassModel
        {
            get { return _classModel; }
            set { this.SetProperty(ref _classModel, value, PropertyChanged); }    
        }
        private Domain.Models.Class _classModel;

        public Domain.Models.AttendanceRecord AttendanceModel
        {
            get { return _attendanceModel; }
            set { this.SetProperty(ref _attendanceModel, value, PropertyChanged); }
        }
        private Domain.Models.AttendanceRecord _attendanceModel;

        public Domain.Models.CanceledClass CanceledModel
        {
            get { return _canceledModel; }
            set { this.SetProperty(ref _canceledModel, value, PropertyChanged); }
        }
        private Domain.Models.CanceledClass _canceledModel;

        public DateTimeOffset Date
        {
            get { return _date; }
            set { this.SetProperty(ref _date, value.UtcDateTime.Date, PropertyChanged); }
        }
        private DateTimeOffset _date;

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set
            {
                this.SetProperty(ref _isCanceled, value, PropertyChanged);
                HandleCanceledClass();
            }
        }
        private bool _isCanceled;

        public bool IsCanceledViewVisible
        {
            get { return _isCanceledViewVisible; }
            set { this.SetProperty(ref _isCanceledViewVisible, value, PropertyChanged); }
        }
        private bool _isCanceledViewVisible;

        public List<string> CanceledOptions
        {
            get { return _canceledOptions ?? (_canceledOptions = new List<string>()); }
            set { this.SetProperty(ref _canceledOptions, value, PropertyChanged); }
        }
        private List<string> _canceledOptions;

        public int CanceledIndex
        {
            get { return _canceledIndex; }
            set { this.SetProperty(ref _canceledIndex, value, PropertyChanged); }
        }
        private int _canceledIndex;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.AttendingClass> AttendingList
        {
            get { return _attending ?? (_attending = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.AttendingClass>()); }
            set { this.SetProperty(ref _attending, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.AttendingClass> _attending;

        public ICommand AttendingCommand => _attendingCommand ?? (_attendingCommand = new Command<string>(HandleAttendance));
        private ICommand _attendingCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
