using System;
using cm.frontend.core.Phone.Views.Pages;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace cm.frontend.core.Phone
{
    public partial class App : Application
    {
        public static EventHandler LaunchMasterDetailPage;
        public static EventHandler LaunchLoginPage;

        public App()
        {
            InitializeComponent();
            Initialize();

            LaunchMasterDetailPage += (sender, args) =>
            {
                LoadMasterDetailPage();
            };

            LaunchLoginPage += (sender, args) =>
            {
                LoadLoginPage();
            };
        }

        private void Initialize()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            object contextObject;
            Current.Properties.TryGetValue("Context", out contextObject);
            Domain.Objects.Context context = null;
            if (contextObject != null)
            {
                context = JsonConvert.DeserializeObject<Domain.Objects.Context>((string)contextObject);
            }

            if (context != null)
            {
                contextCache.Replace("Context", context);
                if (context.IsAuthenticated)
                {
                    LoadMasterDetailPage();
                    var synchronizer = new Domain.Services.Sync.Synchronizer();
                    synchronizer.SyncAllAndContinue();
                }
                else
                {
                    LoadLoginPage();
                }
            }
            else
            {
                var newContext = new Domain.Objects.Context();
                contextCache.Replace("Context", newContext);
                LoadLoginPage();
            }
        }

        public void LoadMasterDetailPage()
        {
            MainPage = new MasterDetail();
        }

        public void LoadLoginPage()
        {
            MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
