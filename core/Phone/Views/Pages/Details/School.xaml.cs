using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class School
    {
        private Base.PageController<ViewModels.Pages.Details.School> PageController { get; }

        public School()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.School>(this);

            if (PageController.ViewModel.UserIsTeacher())
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
