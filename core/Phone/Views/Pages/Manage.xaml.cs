using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Manage
    {
        private Base.PageController<ViewModels.Pages.Manage> PageController { get; }

        public Manage()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Manage>(this);
        }
    }
}
