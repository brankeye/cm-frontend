using System;
using cm.frontend.core.Domain.Models;
using Xamarin.Forms;

namespace cm.frontend.core.Phone
{
    public class App : Application
    {
        public App()
        {
            SeedData();
            var mainPage = new Views.Pages.Login();
            MainPage = new NavigationPage(mainPage);
        }

        private async void SeedData()
        {
            var classesRealm = new Domain.Services.Realms.Classes();
            await classesRealm.WriteAsync(realm =>
            {
                var timesRealm = new Domain.Services.Realms.Times();

                var class1 = realm.CreateObject();
                class1.Name = "Evening Class";
                class1.Day = "Monday";
                var time1 = new Time(19, 30, 0);
                var time2 = new Time(21, 30, 0);
                timesRealm.Manage(time1);
                timesRealm.Manage(time2);
                class1.StartTime = time1;
                class1.EndTime = time2;

                var class2 = realm.CreateObject();
                class2.Name = "Evening Class";
                class2.Day = "Wednesday";
                var time3 = new Time(19, 30, 0);
                var time4 = new Time(21, 30, 0);
                timesRealm.Manage(time3);
                timesRealm.Manage(time4);
                class2.StartTime = time3;
                class2.EndTime = time4;

                var class3 = realm.CreateObject();
                class3.Name = "Afternoon Class";
                class3.Day = "Saturday";
                var time5 = new Time(19, 30, 0);
                var time6 = new Time(21, 30, 0);
                timesRealm.Manage(time5);
                timesRealm.Manage(time6);
                class3.StartTime = time5;
                class3.EndTime = time6;
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
