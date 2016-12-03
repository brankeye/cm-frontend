using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Students
    {
        public Students()
        {
            InitializeComponent();

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
            ViewModel.StudentSelected(student.StudentModel.LocalId);
        }
    }
}
