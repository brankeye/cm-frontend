using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Evaluations : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; set; }

        private void Initialize()
        {
            EvaluationsRealm = new Domain.Services.Realms.Evaluations();
            var evals = EvaluationsRealm.GetAll().ToList();
            var evalsContainer = new List<ViewModels.Controls.PrettyListViewItems.Evaluation>();
            foreach (var evalModel in evals)
            {
                evalsContainer.Add(new ViewModels.Controls.PrettyListViewItems.Evaluation(evalModel));
            }
            EvaluationsList.AddRange(evalsContainer);
        }

        public void OnAppearing()
        {
            Initialize();
        }

        public async void LaunchEvaluation(int evalLocalId)
        {
            var navigator = new Services.Navigator();
            await navigator.PushEvaluationPageAsync(Navigation, evalLocalId);
        }

        public INavigation Navigation { get; set; }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> EvaluationsList
        {
            get { return _evals ?? (_evals = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation>()); }
            set { this.SetProperty(ref _evals, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> _evals;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
