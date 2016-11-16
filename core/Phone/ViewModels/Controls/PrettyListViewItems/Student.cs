using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Student : Base.Core
    {
        public Student(Domain.Models.Profile model)
        {
            StudentModel = model;
        }

        public Domain.Models.Profile StudentModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Profile _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
