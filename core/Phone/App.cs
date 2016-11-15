using cm.frontend.core.Phone.Views.Pages;
using Xamarin.Forms;

namespace cm.frontend.core.Phone
{
    public class App : Application
    {
        public App()
        {
            var mainPage = new Views.Pages.Login();
            MainPage = new NavigationPage(mainPage);
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
