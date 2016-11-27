﻿using cm.frontend.core.Domain.Objects;
using Xamarin.Forms;

namespace cm.frontend.core.Phone
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Initialize();
            var mainPage = new Views.Pages.Login();
            MainPage = new NavigationPage(mainPage);
        }

        private void Initialize()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = new Context();
            contextCache.Replace("Context", currentContext);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
            var synchronizer = new Domain.Services.Sync.Synchronizer();
            await synchronizer.SyncAllAndWait();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
