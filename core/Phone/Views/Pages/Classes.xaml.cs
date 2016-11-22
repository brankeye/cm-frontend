using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Classes
    {
        public Classes()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void ClassesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var classItem = (ViewModels.Controls.PrettyListViewItems.Class) e.SelectedItem;
            ViewModel.ClassSelected(classItem.ClassModel.LocalId);
        }
    }
}
