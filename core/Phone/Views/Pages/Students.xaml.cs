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
