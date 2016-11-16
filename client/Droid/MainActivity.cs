using Android.App;
using Android.Content.PM;
using Android.OS;
using FormsToolkit.Droid;

namespace cm.frontend.client.Droid
{
    [Activity(Label = "Club Manager", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            Xamarin.Forms.Forms.Init(this, bundle);
            Toolkit.Init();

            /*
            Xamarin.Forms.Application app;

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    app = new cm.frontend.core.Phone.App();
                    break;
                case TargetIdiom.Tablet:
                    app = new cm.frontend.core.Tablet.App();
                    break;
                default:
                    app = new cm.frontend.core.Shared.App();
                    break;
            }
            */

            // Always launch the phone app for now
            Xamarin.Forms.Application app = new cm.frontend.core.Phone.App();

            LoadApplication(app);
        }
    }
}

