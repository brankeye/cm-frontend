using System;
using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class ClassDate : Base.Core
    {
        public ClassDate(Domain.Models.Class model, DateTimeOffset date, bool isCanceled)
        {
            ClassModel = model;
            Date = date;
            IsCanceled = isCanceled;
        }

        public DateTimeOffset Date
        {
            get { return _date; }
            set { this.SetProperty(ref _date, value.UtcDateTime.Date, PropertyChanged); }
        }
        private DateTimeOffset _date;

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set { this.SetProperty(ref _isCanceled, value, PropertyChanged); }
        }
        private bool _isCanceled;

        public Domain.Models.Class ClassModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Class _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
