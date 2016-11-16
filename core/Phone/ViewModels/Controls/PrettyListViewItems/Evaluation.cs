using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Evaluation : Base.Core
    {
        public Evaluation(Domain.Models.Evaluation model)
        {
            EvaluationModel = model;
        }

        public Domain.Models.Evaluation EvaluationModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Evaluation _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
