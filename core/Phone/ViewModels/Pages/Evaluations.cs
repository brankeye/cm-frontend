using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Evaluations : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; } = new Domain.Services.Realms.Evaluations();

        public void Initialize(int profileLocalId)
        {
            ProfileLocalId = profileLocalId;
        }

        private void GetEvaluations()
        {
            var profileModel = GetCurrentUser().Profile;
            var membersRealm = new Domain.Services.Realms.Members();
            var memberModel = membersRealm.Get(x => x.Profile == profileModel);
            var evals = EvaluationsRealm.GetAll(x => x.Member == memberModel).ToList();
            var evalsContainer = new List<ViewModels.Controls.PrettyListViewItems.Evaluation>();
            foreach (var evalModel in evals)
            {
                evalsContainer.Add(new ViewModels.Controls.PrettyListViewItems.Evaluation(evalModel));
            }
            EvaluationsList.Clear();
            EvaluationsList.AddRange(evalsContainer);
        }

        public override void OnAppearing()
        {
            GetEvaluations();
        }

        public async void LaunchEvaluation(int evalLocalId)
        {
            await Navigator.PushEvaluationPageAsync(Navigation, evalLocalId);
        }

        private int ProfileLocalId { get; set; }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> EvaluationsList
        {
            get { return _evals ?? (_evals = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation>()); }
            set { this.SetProperty(ref _evals, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> _evals;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
