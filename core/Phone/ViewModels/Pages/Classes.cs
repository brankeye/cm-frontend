using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Classes : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; set; }

        private void Initialize()
        {
            ClassesRealm = new Domain.Services.Realms.Classes();
            var currentSchool = GetCurrentSchool();
            var classes = ClassesRealm.GetAll(x => x.School == currentSchool).ToList();
            var classesContainer = new List<ViewModels.Controls.PrettyListViewItems.Class>();
            foreach (var classModel in classes)
            {
                classesContainer.Add(new ViewModels.Controls.PrettyListViewItems.Class(classModel));
            }
            ClassesList.Clear();
            ClassesList.AddRange(classesContainer);
        }

        public override void OnAppearing()
        {
            Initialize();
        }

        public async void ClassSelected(int classLocalId)
        {
            await Navigator.PushClassEditorPageAsync(Navigation, classLocalId);
        }

        private async void NewClass()
        {
            await Navigator.PushClassEditorPageAsync(Navigation);
        }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class> ClassesList
        {
            get { return _classes ?? (_classes = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class>()); }
            set { this.SetProperty(ref _classes, value, PropertyChanged); }    
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class> _classes;

        public ICommand AddClassCommand => _addClassCommand ?? (_addClassCommand = new Command(NewClass));
        private ICommand _addClassCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
