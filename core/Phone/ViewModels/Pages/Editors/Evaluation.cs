using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class Evaluation : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; } = new Domain.Services.Realms.Evaluations();

        public void InitializeExisting(int evalLocalId)
        {
            EvaluationLocalId = evalLocalId;
            IsEditingExistingEvaluation = true;
        }

        public void InitializeNew(int memberLocalId)
        {
            MemberLocalId = memberLocalId;
            IsEditingExistingEvaluation = false;
        }

        public override void OnAppearing()
        {
            if (IsEditingExistingEvaluation)
            {
                var eval = EvaluationsRealm.Get(EvaluationLocalId);
                Name = eval.Name;
            }
        }

        private async void Save()
        {
            if (!IsValidInput())
            {
                DisplayAlert("Invalid input", "Cannot be empty.");
                return;
            }

            var membersRealm = new Domain.Services.Realms.Members();
            await EvaluationsRealm.WriteAsync(realm =>
            {
                var eval = IsEditingExistingEvaluation ? realm.Get(EvaluationLocalId) : realm.CreateObject();
                eval.Name = Name;
                if (!IsEditingExistingEvaluation)
                {
                    eval.Member = membersRealm.Get(MemberLocalId);
                }
                eval.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            synchronizer.SyncPostsAndContinue();

            await LeavePage();
        }

        private bool IsValidInput()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }
            return true;
        }

        private async void Cancel()
        {
            await LeavePage();
        }

        private async Task LeavePage()
        {
            await Navigator.PopAsync(Navigation);
        }

        private bool IsEditingExistingEvaluation { get; set; }

        private int MemberLocalId { get; set; }

        private int EvaluationLocalId { get; set; }

        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value, PropertyChanged); }
        }
        private string _name;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save));
        private ICommand _saveCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
