using System;
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
            Detail = new NavigationPage(new Views.Pages.Calendar());

            masterPage.MasterListView.ItemSelected += ListView_OnItemSelected;

            masterPage.ProfileTapped += Profile_OnTapped;
        }

        private void Profile_OnTapped(object sender, EventArgs eventArgs)
        {
            NavigationByType(typeof(Views.Pages.Details.Profile));
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MasterItem;
            if (item != null)
            {
                NavigationByType(item.TargetType);
            }
        }

        private void NavigationByType(Type type)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(type));
            IsPresented = false;
        }
    }
}
