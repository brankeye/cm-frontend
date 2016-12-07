using System;
using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Master
    {
        public event EventHandler ProfileTapped;

        private Base.PageController<ViewModels.Pages.Master> PageController { get; }

        public Master()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Master>(this);
        }

        public ListView MasterListView => ItemsListView;

        private void Profile_OnTapped(object sender, EventArgs e)
        {
            ProfileTapped?.Invoke(this, EventArgs.Empty);
        }
    }
}
