using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Class : Base.Core
    {
        public Class(Domain.Models.Class model)
        {
            ClassModel = model;
            DayAndTime = ClassModel.Day + " from " + ClassModel.StartTime.ToString("h:mm tt") + " to " + ClassModel.EndTime.ToString("h:mm tt");
        }

        public string DayAndTime
        {
            get { return _dayAndTime ?? (_dayAndTime = ""); }
            set { this.SetProperty(ref _dayAndTime, value, PropertyChanged); }
        }
        private string _dayAndTime;

        public Domain.Models.Class ClassModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Class _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
