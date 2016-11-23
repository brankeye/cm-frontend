using System;
using Xamarin.Forms;

namespace cm.frontend.core.Phone
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //SeedData();
            TestLogin();
            var mainPage = new Views.Pages.Login();
            MainPage = new NavigationPage(mainPage);
        }

        private async void TestLogin()
        {
            var tokenService = new Domain.Services.Rest.Security.Token();
            var token = await tokenService.PostAsync("testaccount@test.com", "Password1!");
        }

        private async void SeedData()
        {
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

            var classesRealm = new Domain.Services.Realms.Classes();
            await classesRealm.WriteAsync(realm =>
            {
                var class1 = realm.CreateObject();
                class1.Name = "Evening Class";
                class1.Day = "Monday";
                class1.StartTime = new DateTimeOffset(1, 1, 1, 19, 30, 0, TimeSpan.Zero);
                class1.EndTime = new DateTimeOffset(1, 1, 1, 21, 30, 0, TimeSpan.Zero);
                class1.School = schoolsRealm.Get(x => x.Name == "STVTO");

                var class2 = realm.CreateObject();
                class2.Name = "Evening Class";
                class2.Day = "Wednesday";
                class2.StartTime = new DateTimeOffset(1, 1, 1, 19, 30, 0, TimeSpan.Zero);
                class2.EndTime = new DateTimeOffset(1, 1, 1, 21, 30, 0, TimeSpan.Zero);
                class2.School = schoolsRealm.Get(x => x.Name == "STVTO");

                var class3 = realm.CreateObject();
                class3.Name = "Afternoon Class";
                class3.Day = "Saturday";
                class3.StartTime = new DateTimeOffset(1, 1, 1, 11, 30, 0, TimeSpan.Zero);
                class3.EndTime = new DateTimeOffset(1, 1, 1, 13, 30, 0, TimeSpan.Zero);
                class3.School = schoolsRealm.Get(x => x.Name == "STVTO");
            });

            var studentsRealm = new Domain.Services.Realms.Students();
            await studentsRealm.WriteAsync(realm =>
            {
                var students1 = realm.CreateObject();

                var p1 = profilesRealm.Get(x => x.FirstName == "Cameron");
                var s1 = schoolsRealm.Get(x => x.Name == "STVTO");

                students1.Profile = p1;
                students1.School = s1;

                var students2 = realm.CreateObject();

                var p2 = profilesRealm.Get(x => x.FirstName == "Kyle");

                students2.Profile = p2;
                students2.School = s1;
            });

            var evaluationsRealm = new Domain.Services.Realms.Evaluations();
            await evaluationsRealm.WriteAsync(realm =>
            {
                var theTime = DateTimeOffset.Now.TimeOfDay;
                var time = new DateTimeOffset(1, 1, 1, theTime.Hours, theTime.Minutes, theTime.Seconds, TimeSpan.Zero);

                var profile = profilesRealm.Get(x => x.FirstName == "Cameron");
                var school = schoolsRealm.Get(x => x.Name == "STVTO");

                var eval1 = realm.CreateObject();
                eval1.Name = "Siu Lim Tao";
                eval1.Date = DateTimeOffset.Now.Date;
                eval1.Time = time;
                eval1.Student = studentsRealm.Get(x => x.Profile == profile && x.School == school);

                var eval2 = realm.CreateObject();
                eval2.Name = "Chum Kiu";
                eval2.Date = DateTimeOffset.Now.Date;
                eval2.Time = time;
                eval2.Student = studentsRealm.Get(x => x.Profile == profile && x.School == school);

                var eval3 = realm.CreateObject();
                eval3.Name = "Biu Jee";
                eval3.Date = DateTimeOffset.Now.Date;
                eval3.Time = time;
                eval3.Student = studentsRealm.Get(x => x.Profile == profile && x.School == school);
            });

            var sectionsRealm = new Domain.Services.Realms.EvaluationSections();
            await sectionsRealm.WriteAsync(realm =>
            {
                var eval = evaluationsRealm.Get(x => x.Name == "Siu Lim Tao");

                var section1 = realm.CreateObject();
                section1.Name = "1st Part";
                section1.Body = "Needs work.";
                section1.Score = 0;
                section1.Evaluation = eval;

                var section2 = realm.CreateObject();
                section2.Name = "2nd Part";
                section2.Body = "Needs more work.";
                section2.Score = 0;
                section2.Evaluation = eval;

                var section3 = realm.CreateObject();
                section3.Name = "3rd Part";
                section3.Body = "Needs work still.";
                section3.Score = 0;
                section3.Evaluation = eval;
            });

            var attendingRealm = new Domain.Services.Realms.AttendingClasses();
            await attendingRealm.WriteAsync(realm =>
            {
                var attending1 = realm.CreateObject();
                attending1.Date = DateTimeOffset.Now.Date;
                attending1.Class = classesRealm.Get(x => x.Name == "Afternoon Class");
                attending1.Profile = profilesRealm.Get(x => x.FirstName == "Kyle");
            });

            var accountsRealm = new Domain.Services.Realms.Accounts();
            await accountsRealm.WriteAsync(realm =>
            {
                var user = realm.CreateObject();
                var profile = profilesRealm.Get(x => x.FirstName == "Brandon");
                user.Profile = profile;
            });

            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var context = new Domain.Models.Local.Context
            {
                CurrentAccount = accountsRealm.Get(1)
            };


            var teacherProfile = schoolsRealm.Get(x => x.Name == "STVTO").Teacher;
            if (context.CurrentAccount.Profile.LocalId == teacherProfile.LocalId)
            {
                context.IsTeacher = true;
            }

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
