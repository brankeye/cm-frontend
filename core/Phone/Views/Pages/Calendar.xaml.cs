using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Calendar
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private void ClassesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var classItem = (ViewModels.Controls.PrettyListViewItems.ClassDate)e.SelectedItem;
            ViewModel.ClassSelected(classItem.ClassModel.LocalId, classItem.Date);
        }
    }
}
