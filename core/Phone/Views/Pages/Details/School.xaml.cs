using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class School
    {
        public School()
        {
            InitializeComponent();

            if (ViewModel.UserIsTeacher())
            {
                var editToolbar = new ToolbarItem()
                {
                    Text = "Edit",
                    Order = ToolbarItemOrder.Primary
                };
                editToolbar.SetBinding(MenuItem.CommandProperty, new Binding("EditSchoolCommand"));
                ToolbarItems.Add(editToolbar);
            }
        }
    }
}
