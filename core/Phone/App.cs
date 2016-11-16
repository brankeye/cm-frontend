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

            var profilesRealm = new Domain.Services.Realms.Profiles();
            await profilesRealm.WriteAsync(realm =>
            {
                var profile1 = realm.CreateObject();
                profile1.FirstName = "Brandon";
                profile1.LastName = "Keyes";

                var profile2 = realm.CreateObject();
                profile2.FirstName = "Cameron";
                profile2.LastName = "Keyes";

                var profile3 = realm.CreateObject();
                profile3.FirstName = "Kyle";
                profile3.LastName = "Keyes";
            });

            var schoolsRealm = new Domain.Services.Realms.Schools();
            await schoolsRealm.WriteAsync(realm =>
            {
                var school1 = realm.CreateObject();
                school1.Name = "STVTO";

                var p1 = profilesRealm.Get(x => x.FirstName == "Brandon");
                school1.Teacher = p1;
            });

            var studentsRealm = new Domain.Services.Realms.Students();
            await studentsRealm.WriteAsync(realm =>
            {
                var students1 = realm.CreateObject();

                var p1 = profilesRealm.Get(x => x.FirstName == "Cameron");
                var s1 = schoolsRealm.Get(x => x.Name == "STVTO");

                students1.Student = p1;
                students1.School = s1;

                var students2 = realm.CreateObject();

                var p2 = profilesRealm.Get(x => x.FirstName == "Kyle");

                students2.Student = p2;
                students2.School = s1;
            });

            var evaluationsRealm = new Domain.Services.Realms.Evaluations();
            await evaluationsRealm.WriteAsync(realm =>
            {
                var eval1 = realm.CreateObject();
                eval1.Name = "Evaluation 1";
                eval1.Date = DateTimeOffset.Now;

                var eval2 = realm.CreateObject();
                eval2.Name = "Evaluation 2";
                eval2.Date = DateTimeOffset.Now;

                var eval3 = realm.CreateObject();
                eval3.Name = "Evaluation 3";
                eval3.Date = DateTimeOffset.Now;
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
