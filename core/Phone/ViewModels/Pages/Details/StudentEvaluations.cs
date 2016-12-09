using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class StudentEvaluations : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; } = new Domain.Services.Realms.Evaluations();

        public void Initialize(int profileLocalId)
        {
            ProfileLocalId = profileLocalId;
            RefreshData();
        }

        public override void RefreshData()
        {
            var profilesRealm = new Domain.Services.Realms.Profiles();
            ProfileModel = profilesRealm.Get(ProfileLocalId);
            var membersRealm = new Domain.Services.Realms.Members();
            var member = membersRealm.Get(x => x.Profile == ProfileModel);
            var evals = EvaluationsRealm.GetAll(x => x.Member == member).ToList();
            var evalsContainer = new List<ViewModels.Controls.PrettyListViewItems.Evaluation>();
            foreach (var evalModel in evals)
            {
                evalsContainer.Add(new ViewModels.Controls.PrettyListViewItems.Evaluation(evalModel));
            }
            EvaluationsList.Clear();
            EvaluationsList.AddRange(evalsContainer);
        }

        public async void LaunchEvaluation(int evalLocalId)
        {
            await Navigator.PushEvaluationPageAsync(Navigation, evalLocalId);
        }

        private async void AddEvaluation()
        {
            var membersRealm = new Domain.Services.Realms.Members();
            var member = membersRealm.Get(x => x.Profile == ProfileModel);
            await Navigator.PushNewEvaluationEditorPageAsync(Navigation, member.LocalId);
        }

        public bool IsTeacher()
        {
            return UserIsTeacher();
        }

        private int ProfileLocalId { get; set; }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profile; }
            set { this.SetProperty(ref _profile, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profile;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> EvaluationsList
        {
            get { return _evals ?? (_evals = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation>()); }
            set { this.SetProperty(ref _evals, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Evaluation> _evals;

        public ICommand AddCommand => _addCommand ?? (_addCommand = new Command(AddEvaluation));
        private ICommand _addCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
