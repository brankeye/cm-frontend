using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Models;
using cm.frontend.core.Domain.Utilities;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Classes : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Classes ClassesRealm { get; set; }

        private void Initialize()
        {
            ClassesRealm = new Domain.Services.Realms.Classes();
            var classes = ClassesRealm.GetAll().ToList();
            var classesContainer = new List<ViewModels.Controls.PrettyListViewItems.Class>();
            foreach (var classModel in classes)
            {
                classesContainer.Add(new ViewModels.Controls.PrettyListViewItems.Class(classModel));
            }
            ClassesList.AddRange(classesContainer);
        }

        public void OnAppearing()
        {
            Initialize();
        }

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class> ClassesList
        {
            get { return _classes ?? (_classes = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class>()); }
            set { this.SetProperty(ref _classes, value, PropertyChanged); }    
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.Class> _classes;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
