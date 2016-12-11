using System.ComponentModel;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Class : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; } = new Domain.Services.Realms.Classes();

        public void Initialize(int classLocalId)
        {
            ClassId = classLocalId;
        }

        public override void RefreshData()
        {
            ClassModel = ClassesRealm.Get(ClassId);
        }

        private async void EditClass()
        {
            await Navigator.PushClassEditorPageAsync(Navigation, ClassId);
        }

        public int ClassId { get; set; }

        public Domain.Models.Class ClassModel
        {
            get { return _class; }
            set { this.SetProperty(ref _class, value, PropertyChanged, true); }
        }
        private Domain.Models.Class _class;

        public ICommand EditClassCommand => _editClassCommand ?? (_editClassCommand = new Command(EditClass));
        private ICommand _editClassCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
