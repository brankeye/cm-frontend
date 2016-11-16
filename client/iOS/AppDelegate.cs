using FormsToolkit.iOS;
using Foundation;
using UIKit;

namespace cm.frontend.client.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
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
            Xamarin.Forms.Application formsApp = new cm.frontend.core.Phone.App();

            LoadApplication(formsApp);

            return base.FinishedLaunching(app, options);
        }
    }
}
