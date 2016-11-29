using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class MasterDetail
    {
        public MasterDetail()
        {
            InitializeComponent();
            var masterPage = new Views.Pages.Master();
            Master = masterPage;
            Detail = new NavigationPage(new Views.Pages.Dashboard());

            masterPage.MasterListView.ItemSelected += ListView_OnItemSelected;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MasterItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                IsPresented = false;
            }
        }
    }
}
