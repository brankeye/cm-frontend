using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Classes
    {
        private Base.PageController<ViewModels.Pages.Classes> PageController { get; }

        public Classes()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Classes>(this);
        }

        private void ClassesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var classItem = (ViewModels.Controls.PrettyListViewItems.Class) e.SelectedItem;
            PageController.ViewModel.ClassSelected(classItem.ClassModel.LocalId);
        }
    }
}
