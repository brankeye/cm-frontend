using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class EvaluationSection : ViewModels.Base.Core, INotifyPropertyChanged
    {
        public void Initialize(int sectionLocalId)
        {
            SectionLocalId = sectionLocalId;
            IsEditingExistingSection = true;
            var sectionsRealm = new Domain.Services.Realms.EvaluationSections();
            var item = sectionsRealm.Get(SectionLocalId);
            Name = item.Name;
            Body = item.Body;
            Score = item.Score;
        }

        private async void SaveSection()
        {
            var sectionsRealm = new Domain.Services.Realms.EvaluationSections();
            await sectionsRealm.WriteAsync(realm =>
            {
                var item = IsEditingExistingSection ? realm.Get(SectionLocalId) : realm.CreateObject();
                item.Name = Name;
                item.Body = Body;
                item.Score = Score;
            });

            var navigator = new Services.Navigator();
            await navigator.PopAsync(Navigation);
        }

        private async void Cancel()
        {
            var navigator = new Services.Navigator();
            await navigator.PopAsync(Navigation);
        }

        private int SectionLocalId { get; set; }

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
