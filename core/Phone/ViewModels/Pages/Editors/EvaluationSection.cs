using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class EvaluationSection : ViewModels.Base.Core, INotifyPropertyChanged
    {
        public void Initialize(int sectionLocalId, int evaluationLocalId, bool isEditingExistingSection)
        {
            IsEditingExistingSection = isEditingExistingSection;
            SectionLocalId = sectionLocalId;
            EvaluationLocalId = evaluationLocalId;

            if (IsEditingExistingSection)
            {
                var sectionsRealm = new Domain.Services.Realms.EvaluationSections();
                var item = sectionsRealm.Get(SectionLocalId);
                Name = item.Name;
                Body = item.Body;
                Score = item.Score;
            }
        }

        private async void SaveSection()
        {
            var sectionsRealm = new Domain.Services.Realms.EvaluationSections();
            var evaluationsRealm = new Domain.Services.Realms.Evaluations();
            await sectionsRealm.WriteAsync(realm =>
            {
                var item = IsEditingExistingSection ? realm.Get(SectionLocalId) : realm.CreateObject();
                item.Name = Name;
                item.Body = Body;
                item.Score = Score;
                item.Evaluation = evaluationsRealm.Get(EvaluationLocalId);
                item.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            synchronizer.SyncPostsAndContinue();

            await LeavePage();
        }

        private async void Cancel()
        {
            await LeavePage();
        }

        private async Task LeavePage()
        {
            await Navigator.PopAsync(Navigation);
        }

        private int SectionLocalId { get; set; }

        private int EvaluationLocalId { get; set; }

        private bool IsEditingExistingSection { get; set; }

        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value, PropertyChanged); }
        }
        private string _name;

        public float Score
        {
            get { return _score; }
            set { this.SetProperty(ref _score, value, PropertyChanged); }
        }
        private float _score;

        public string Body
        {
            get { return _body; }
            set { this.SetProperty(ref _body, value, PropertyChanged); }
        }
        private string _body;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveSection));
        private ICommand _saveCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
