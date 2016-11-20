using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class EvaluationSection : ContentPage
    {
        public ViewModels.Pages.Editors.EvaluationSection ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Editors.EvaluationSection);
        private ViewModels.Pages.Editors.EvaluationSection _vm;

        public EvaluationSection()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Editors.EvaluationSection();
        }

        public void Initialize(int sectionLocalId)
        {
            ViewModel.Initialize(sectionLocalId);
        }
    }
}
