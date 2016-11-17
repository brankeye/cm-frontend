using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Students : ContentPage
    {
        public ViewModels.Pages.Students ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Students);
        private ViewModels.Pages.Students _vm;

        public Students()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Students();

            // TODO: Add student to club
            /*
            var addStudentToolbarItem = new ToolbarItem("Add student", "ic_add_white_48dp.png", null, ToolbarItemOrder.Primary);
            addStudentToolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("AddStudentCommand"));
            ToolbarItems.Add(addStudentToolbarItem);
            */
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void StudentsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var student = (ViewModels.Controls.PrettyListViewItems.Student) e.SelectedItem;
            ViewModel.StudentSelected(student.StudentModel.LocalId);
        }
    }
}
