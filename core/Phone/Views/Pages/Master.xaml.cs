using System;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Master
    {
        public event EventHandler ProfileTapped;

        public Master()
        {
            InitializeComponent();
        }

        public ListView MasterListView => ItemsListView;

        private void Profile_OnTapped(object sender, EventArgs e)
        {
            ProfileTapped?.Invoke(this, EventArgs.Empty);
        }
    }
}
