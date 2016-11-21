using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class StudentEvaluations : ContentPage
    {
        public ViewModels.Pages.Details.StudentEvaluations ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Details.StudentEvaluations);
        private ViewModels.Pages.Details.StudentEvaluations _vm;

        public StudentEvaluations()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Details.StudentEvaluations();
            var header = (View) EvaluationsListView.Header;
            header.BindingContext = BindingContext;
        }

        public void Initialize(int profileLocalId)
        {
            ViewModel.Initialize(profileLocalId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void Evaluations_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var eval = (ViewModels.Controls.PrettyListViewItems.Evaluation)e.SelectedItem;
            ViewModel.LaunchEvaluation(eval.EvaluationModel.LocalId);
        }
    }
}
