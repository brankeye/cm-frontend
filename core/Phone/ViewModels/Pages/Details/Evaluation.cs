using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Evaluation : INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; set; }

        private Domain.Services.Realms.EvaluationSections SectionsRealm { get; set; }

        public void Initialize(int evalLocalId)
        {
            EvaluationLocalId = evalLocalId;
        }

        public void OnAppearing()
        {
            EvaluationsRealm = new Domain.Services.Realms.Evaluations();
            EvaluationModel = EvaluationsRealm.Get(EvaluationLocalId);

            SectionsRealm = new EvaluationSections();
            var sections = SectionsRealm.GetAll(x => x.Evaluation == EvaluationModel).ToList();
            var sectionsContainer = new List<ViewModels.Controls.PrettyListViewItems.EvaluationSection>();
            
            foreach(var sec in sections)
            {
                sectionsContainer.Add(new ViewModels.Controls.PrettyListViewItems.EvaluationSection(sec));
            }

            SectionsList.Clear();
            SectionsList.AddRange(sectionsContainer);
        }

        public async void EditSection(int sectionLocalId)
        {
            var navigator = new Services.Navigator();
            await navigator.PushEvaluationSectionEditorPageAsync(Navigation, sectionLocalId);
        }

        private int EvaluationLocalId { get; set; }

        public Domain.Models.Evaluation EvaluationModel
        {
            get { return _evaluation; }
            set { this.SetProperty(ref _evaluation, value, PropertyChanged); }
        }
        private Domain.Models.Evaluation _evaluation;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection> SectionsList
        {
            get { return _sections ?? (_sections = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection>()); }
            set { this.SetProperty(ref _sections, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection> _sections;

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
