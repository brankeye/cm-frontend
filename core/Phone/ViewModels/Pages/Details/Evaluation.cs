﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using cm.frontend.core.Domain.Utilities;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Details
{
    public class Evaluation : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Evaluations EvaluationsRealm { get; } = new Domain.Services.Realms.Evaluations();

        private Domain.Services.Realms.EvaluationSections SectionsRealm { get; } = new EvaluationSections();

        public void Initialize(int evalLocalId)
        {
            EvaluationLocalId = evalLocalId;
        }

        public override void RefreshData()
        {
            EvaluationModel = EvaluationsRealm.Get(EvaluationLocalId);

            var sections = SectionsRealm.GetAll(x => x.Evaluation == EvaluationModel).ToList();
            var sectionsContainer = new List<ViewModels.Controls.PrettyListViewItems.EvaluationSection>();
            var overallScore = 0.0f;
            foreach (var sec in sections)
            {
                overallScore += sec.Score;
                sectionsContainer.Add(new ViewModels.Controls.PrettyListViewItems.EvaluationSection(sec));
            }
            OverallScore = overallScore;

            SectionsList.Clear();
            SectionsList.AddRange(sectionsContainer);
        }

        public async void EditSection(int sectionLocalId)
        {
            await Navigator.PushEvaluationSectionEditorPageAsync(Navigation, sectionLocalId, EvaluationLocalId);
        }

        private async void AddSection()
        {
            await Navigator.PushEvaluationSectionEditorPageAsync(Navigation, 0, EvaluationLocalId, false);
        }

        private async void EditEvaluation()
        {
            await Navigator.PushExistingEvaluationEditorPageAsync(Navigation, EvaluationLocalId);
        }

        public bool IsTeacher()
        {
            return UserIsTeacher();
        }

        private int EvaluationLocalId { get; set; }

        public float OverallScore
        {
            get { return _score; }
            set { this.SetProperty(ref _score, value, PropertyChanged); }
        }
        private float _score;

        public Domain.Models.Evaluation EvaluationModel
        {
            get { return _evaluation; }
            set { this.SetProperty(ref _evaluation, value, PropertyChanged, true); }
        }
        private Domain.Models.Evaluation _evaluation;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection> SectionsList
        {
            get { return _sections ?? (_sections = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection>()); }
            set { this.SetProperty(ref _sections, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.EvaluationSection> _sections;

        public ICommand EditCommand => _editCommand ?? (_editCommand = new Command(EditEvaluation));
        private ICommand _editCommand;

        public ICommand AddSectionCommand => _addSectionCommand ?? (_addSectionCommand = new Command(AddSection));
        private ICommand _addSectionCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
