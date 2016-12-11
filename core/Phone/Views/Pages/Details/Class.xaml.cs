using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class Class
    {
        private Base.PageController<ViewModels.Pages.Details.Class> PageController { get; }

        public Class()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.Class>(this);
    }

        public void Initialize(int classLocalId)
        {
            PageController.ViewModel.Initialize(classLocalId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (ToolbarItems.Count > 0) return;
            if (PageController.ViewModel.GetCurrentMember().IsTeacher)
            {
                var editClassButton = new ToolbarItem
                {
                    Text = "Edit",
                    Order = ToolbarItemOrder.Primary,
                };
                editClassButton.SetBinding(MenuItem.CommandProperty, new Binding("EditClassCommand"));
                ToolbarItems.Add(editClassButton);
            }
        }
    }
}
