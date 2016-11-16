using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Class : Base.Core
    {
        public Class(Domain.Models.Class model)
        {
            ClassModel = model;
        }

        public Domain.Models.Class ClassModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Class _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
