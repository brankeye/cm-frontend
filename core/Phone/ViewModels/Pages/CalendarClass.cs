using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class CalendarClass : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; set; }

        private Domain.Services.Realms.AttendanceRecords AttendanceRealm { get; set; }

        public CalendarClass()
        {
            AttendingOptions = new List<string>()
            {
                "Undecided",
                "Yes",
                "No"
            };

            CanceledOptions = new List<string>()
            {
                "Yes",
                "No"
            };
            CanceledIndex = 1;
        }

        public void Initialize(int classLocalId, DateTimeOffset date)
        {
            ClassLocalId = classLocalId;
            Date = date.Date;
        }

        public override void OnAppearing()
        {
            ClassesRealm = new Domain.Services.Realms.Classes();
            ClassModel = ClassesRealm.Get(ClassLocalId);
            AttendanceRealm = new Domain.Services.Realms.AttendanceRecords();
            GetAttendants();
            
            var currentProfile = GetCurrentUser().Profile;

            AttendanceModel = AttendanceRealm.GetRealmResults()
                                              .Where(x => x.Date == Date)
                                              .FirstOrDefault(x => x.Profile == currentProfile);

            if (AttendanceModel != null)
            {
                AttendingIndex = AttendingOptions.IndexOf(AttendanceModel.IsAttending ? "Yes" : "No");
            }

            var canceledRealm = new Domain.Services.Realms.CanceledClasses();
            CanceledModel = canceledRealm.GetRealmResults().Where(x => x.Class == ClassModel).FirstOrDefault(x => x.Date == Date);
            if (CanceledModel != null) IsCanceled = true;
        }

        private void GetAttendants()
        {
            var attendants = AttendanceRealm.GetAll(x => x.Date == Date).ToList();

            var attList = new List<ViewModels.Controls.PrettyListViewItems.AttendingClass>();
            foreach (var attendant in attendants)
            {
                var item = new ViewModels.Controls.PrettyListViewItems.AttendingClass(attendant);
                attList.Add(item);
            }

            AttendingList.Clear();
            AttendingList.AddRange(attList);
        }

        public async void HandleCanceledClass()
        {
            var canceledRealm = new Domain.Services.Realms.CanceledClasses();
            CanceledModel = canceledRealm.GetRealmResults().Where(x => x.Class == ClassModel).FirstOrDefault(x => x.Date == Date);

            if (IsCanceled)
            {
                if (CanceledModel == null)
                {
                    await canceledRealm.WriteAsync(realm =>
                    {
                        var record = new Domain.Models.CanceledClass
                        {
                            Date = Date.Date
                        };
                        realm.Manage(record);
                        record.Class = ClassModel;
                    });
                }
            }
            else
            {
                if (CanceledModel != null)
                {
                    var canceledClassLocalId = CanceledModel.LocalId;
                    await canceledRealm.WriteAsync(realm =>
                    {
                        realm.Remove(canceledClassLocalId);
                    });
                }
            }
        }

        public async void HandleAttendance()
        {
            var attendanceSelection = AttendingOptions[AttendingIndex];
            var currentProfile = GetCurrentUser().Profile;

            // the user has decided to change their attendance, so we first have to check if
            // a record of their attendance exists for this particular class
            if (AttendanceRealm == null)
            {
                return;
            }

            AttendanceModel = AttendanceRealm.GetRealmResults()
                                              .Where(x => x.Date == Date)
                                              .FirstOrDefault(x => x.Profile == currentProfile);

            if (AttendanceModel == null)
            {
                // the user has no previous attendance record for this class
                // so add one, unless they selected "undecided"
                if (attendanceSelection != "Undecided")
                {
                    await AttendanceRealm.WriteAsync(realm =>
                    {
                        var attending = realm.CreateObject();
                        attending.Class = ClassModel;
                        attending.Date = Date.Date;
                        attending.Profile = currentProfile;
                        attending.IsAttending = attendanceSelection == "Yes";
                    });
                }
            }
            else
            {
                var attendanceRecordLocalId = AttendanceModel.LocalId;
                // the user has an attendance record so all we need to do is alter that one
                if (attendanceSelection == "Undecided")
                {
                    await AttendanceRealm.WriteAsync(realm =>
                    {
                        realm.Remove(attendanceRecordLocalId);
                    });
                }
                else
                {
                    await AttendanceRealm.WriteAsync(realm =>
                    {
                        var attendanceRecord = realm.Get(attendanceRecordLocalId);
                        attendanceRecord.IsAttending = attendanceSelection == "Yes";
                    });
                }
            }
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
            set { this.SetProperty(ref _date, value, PropertyChanged); }
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

        public List<string> AttendingOptions
        {
            get { return _attendingOptions ?? (_attendingOptions = new List<string>()); }
            set { this.SetProperty(ref _attendingOptions, value, PropertyChanged); }
        }
        private List<string> _attendingOptions;

        public int AttendingIndex
        {
            get { return _attendingIndex; }
            set
            {
                this.SetProperty(ref _attendingIndex, value, PropertyChanged);
                HandleAttendance();
            }
        }
        private int _attendingIndex;

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
