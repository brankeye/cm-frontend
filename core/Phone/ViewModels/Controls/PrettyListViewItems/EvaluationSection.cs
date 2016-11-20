using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class EvaluationSection : Base.Core
    {
        public EvaluationSection(Domain.Models.EvaluationSection model)
        {
            SectionModel = model;
        }

        public Domain.Models.EvaluationSection SectionModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.EvaluationSection _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
