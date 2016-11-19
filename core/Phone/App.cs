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
                var class1 = realm.CreateObject();
                class1.Name = "Evening Class";
                class1.Day = "Monday";
                class1.StartTime = new DateTimeOffset(1, 1, 1, 19, 30, 0, TimeSpan.Zero);
                class1.EndTime = new DateTimeOffset(1, 1, 1, 21, 30, 0, TimeSpan.Zero);

                var class2 = realm.CreateObject();
                class2.Name = "Evening Class";
                class2.Day = "Wednesday";
                class2.StartTime = new DateTimeOffset(1, 1, 1, 19, 30, 0, TimeSpan.Zero);
                class2.EndTime = new DateTimeOffset(1, 1, 1, 21, 30, 0, TimeSpan.Zero);

                var class3 = realm.CreateObject();
                class3.Name = "Afternoon Class";
                class3.Day = "Saturday";
                class3.StartTime = new DateTimeOffset(1, 1, 1, 11, 30, 0, TimeSpan.Zero);
                class3.EndTime = new DateTimeOffset(1, 1, 1, 13, 30, 0, TimeSpan.Zero);
            });

            var profilesRealm = new Domain.Services.Realms.Profiles();
            await profilesRealm.WriteAsync(realm =>
            {
                var profile1 = realm.CreateObject();
                profile1.FirstName = "Brandon";
                profile1.LastName = "Keyes";
                profile1.Email = "brankeye@gmail.com";
                profile1.PhoneNumber = "6137095799";

                var profile2 = realm.CreateObject();
                profile2.FirstName = "Cameron";
                profile2.LastName = "Keyes";
                profile2.Email = "camisson@gmail.com";
                profile2.PhoneNumber = "6133676789";

                var profile3 = realm.CreateObject();
                profile3.FirstName = "Kyle";
                profile3.LastName = "Keyes";
                profile3.Email = "kylekeye@gmail.com";
                profile3.PhoneNumber = "6134063322";
            });

            var schoolsRealm = new Domain.Services.Realms.Schools();
            await schoolsRealm.WriteAsync(realm =>
            {
                var school1 = realm.CreateObject();
                school1.Name = "STVTO";
                school1.Email = "stvto@gmail.com";
                school1.Address = "546 Place St.";
                school1.PhoneNumber = "6134563456";
                school1.Website = "stvto.com";

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

            var attendingRealm = new Domain.Services.Realms.AttendingClasses();
            await attendingRealm.WriteAsync(realm =>
            {
                var attending1 = realm.CreateObject();
                attending1.Date = DateTimeOffset.Now.AddDays(1).Date;
                attending1.Class = classesRealm.Get(x => x.Name == "Afternoon Class");
                attending1.Profile = profilesRealm.Get(x => x.FirstName == "Kyle");
            });

            var usersRealm = new Domain.Services.Realms.Users();
            await usersRealm.WriteAsync(realm =>
            {
                var user = realm.CreateObject();
                user.Profile = profilesRealm.Get(1);
            });

            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var context = new Domain.Models.Local.Context { CurrentUser = usersRealm.Get(1) };
            contextCache.Add("Context", context);
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
