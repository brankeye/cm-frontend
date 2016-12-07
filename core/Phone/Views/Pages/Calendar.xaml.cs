using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Calendar
    {
        private Base.PageController<ViewModels.Pages.Calendar> PageController { get; }

        public Calendar()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Calendar>(this);
        }

        private void ClassesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var classItem = (ViewModels.Controls.PrettyListViewItems.ClassDate)e.SelectedItem;
            PageController.ViewModel.ClassSelected(classItem.ClassModel.LocalId, classItem.Date);
        }
    }
}
