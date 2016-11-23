using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class AttendingClass : Base.Core
    {
        public AttendingClass(Domain.Models.AttendanceRecord model)
        {
            AttendingModel = model;
            
            Attendance = AttendingModel.IsAttending ? "Yes" : "No";
        }

        public string Attendance
        {
            get { return _attending; }
            set { this.SetProperty(ref _attending, value, PropertyChanged); }
        }
        private string _attending;

        public Domain.Models.AttendanceRecord AttendingModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.AttendanceRecord _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
