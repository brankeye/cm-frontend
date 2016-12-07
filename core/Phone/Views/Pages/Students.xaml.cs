using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Students
    {
        private Base.PageController<ViewModels.Pages.Students> PageController { get; }

        public Students()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Students>(this);

            // TODO: Add student to club
            /*
            var addStudentToolbarItem = new ToolbarItem("Add student", "ic_add_white_48dp.png", null, ToolbarItemOrder.Primary);
            addStudentToolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("AddStudentCommand"));
            ToolbarItems.Add(addStudentToolbarItem);
            */
        }

        private void StudentsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var student = (ViewModels.Controls.PrettyListViewItems.Student) e.SelectedItem;
            PageController.ViewModel.StudentSelected(student.StudentModel.LocalId);
        }
    }
}
