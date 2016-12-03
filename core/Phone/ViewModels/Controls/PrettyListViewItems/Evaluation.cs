using System;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Evaluation : Base.Core
    {
        public Evaluation(Domain.Models.Evaluation model)
        {
            EvaluationModel = model;
            DateTime = new DateTimeOffset(EvaluationModel.Date.Year, EvaluationModel.Date.Month, EvaluationModel.Date.Day,
                                          EvaluationModel.Time.Hour, EvaluationModel.Time.Minute, EvaluationModel.Time.Second, TimeSpan.Zero);

            var evalSectionsRealm = new Domain.Services.Realms.EvaluationSections();
            var evalSections = evalSectionsRealm.GetAll(x => x.Evaluation == EvaluationModel).ToList();
            foreach (var section in evalSections)
            {
                OverallScore += section.Score;
            }
        }

        public float OverallScore
        {
            get { return _overallScore; }
            set { this.SetProperty(ref _overallScore, value, PropertyChanged); }
        }
        private float _overallScore;

        public DateTimeOffset DateTime
        {
            get { return _dateTime; }
            set { this.SetProperty(ref _dateTime, value, PropertyChanged); }
        }
        private DateTimeOffset _dateTime;

        public Domain.Models.Evaluation EvaluationModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Domain.Models.Evaluation _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
