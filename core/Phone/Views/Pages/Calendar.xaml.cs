using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Calendar : ContentPage
    {
        public ViewModels.Pages.Calendar ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Calendar);
        private ViewModels.Pages.Calendar _vm;

        public Calendar()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Calendar();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void ClassesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var classItem = (ViewModels.Controls.PrettyListViewItems.ClassDate)e.SelectedItem;
            ViewModel.ClassSelected(classItem.ClassModel.LocalId, classItem.Date);
        }
    }
}
