using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using cm.frontend.core.Domain.Services.Realms;
using cm.frontend.core.Domain.Utilities;
using cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems;
using Realms;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Master : ViewModels.Base.Core, INotifyPropertyChanged
    {
        private Domain.Services.Realms.Profiles ProfilesRealm { get; set; }

        public Master()
        {
            var isTeacher = GetCurrentMember().IsTeacher;

            ItemsList.Add(new MasterItem
            {
                Title = "My classes",
                IconSource = "myclasses.png",
                TargetType = typeof(Views.Pages.Calendar)
            });
            if (isTeacher)
            {
                ItemsList.Add(new MasterItem
                {
                    Title = "Schedule",
                    IconSource = "schedule.png",
                    TargetType = typeof(Views.Pages.Classes)
                });
            }
            if (!isTeacher)
            {
                ItemsList.Add(new MasterItem
                {
                    Title = "Evaluations",
                    IconSource = "evaluations.png",
                    TargetType = typeof(Views.Pages.Evaluations)
                });
            }
            ItemsList.Add(new MasterItem
            {
                Title = "Students",
                IconSource = "students.png",
                TargetType = typeof(Views.Pages.Students)
            });
            ItemsList.Add(new MasterItem
            {
                Title = "School",
                IconSource = "school.png",
                TargetType = typeof(Views.Pages.Details.School)
            });
        }

        public override void OnAppearing()
        {
            ProfilesRealm = new Profiles();
            ProfileModel = ProfilesRealm.Get(GetCurrentUser().Profile.LocalId);
            SelectedItem = ItemsList[0];
        }

        private async void Signout()
        {
            var accountService = new Domain.Services.Rest.Security.Account();
            var logoutResult = await accountService.PostLogoutAsync(GetContext().AccessToken.Access_Token);

            if (logoutResult.IsSuccessStatusCode)
            {
                var synchronizer = new Domain.Services.Sync.Synchronizer();
                await synchronizer.SyncAllAndWait();

                SaveContext(null, null, false);

                var config = Realm.GetInstance().Config;
                Realm.DeleteRealm(config);

                Application.Current.Properties["Context"] = null;

                App.LaunchLoginPage?.Invoke(this, EventArgs.Empty);
            }
        }

        public Domain.Models.Profile ProfileModel
        {
            get { return _profileModel; }
            set { this.SetProperty(ref _profileModel, value, PropertyChanged); }
        }
        private Domain.Models.Profile _profileModel;

        public ViewModels.Controls.PrettyListViewItems.MasterItem SelectedItem
        {
            get { return _selectedItem; }
            set { this.SetProperty(ref _selectedItem, value, PropertyChanged); }
        }
        private ViewModels.Controls.PrettyListViewItems.MasterItem _selectedItem;

        public DynamicCollection<ViewModels.Controls.PrettyListViewItems.MasterItem> ItemsList
        {
            get { return _itemsList ?? (_itemsList = new DynamicCollection<ViewModels.Controls.PrettyListViewItems.MasterItem>()); }
            set { this.SetProperty(ref _itemsList, value, PropertyChanged); }
        }
        private DynamicCollection<ViewModels.Controls.PrettyListViewItems.MasterItem> _itemsList;

        public ICommand SignoutCommand => _signoutCommand ?? (_signoutCommand = new Command(Signout));
        private ICommand _signoutCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
