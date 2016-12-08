﻿using System;
using System.Reflection;
using Android.App;
using Android.Content.PM;
using Android.OS;
using UXDivers.Gorilla;

namespace cm.frontend.client.Droid
{
    [Activity(Label = "Rassler", MainLauncher = true, Icon = "@drawable/ic_launcher", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            Xamarin.Forms.Forms.Init(this, bundle);

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
#if GORILLA
            var config = new Config("Gorilla on DESKTOP-70FV0JQ");
            config.RegisterAssemblyFromType<Xamarin.Forms.IValueConverter>();
            config.RegisterAssemblyFromType<cm.frontend.core.Domain.Utilities.Converters.ToDateTime>();
            config.RegisterAssemblyFromType<cm.frontend.core.Phone.Views.Behaviors.RegexValidator>();
            config.RegisterAssemblyFromType<cm.frontend.core.Phone.Views.Behaviors.EmailValidator>();
            config.RegisterAssemblyFromType<cm.frontend.core.Phone.Views.Behaviors.PasswordValidator>();
            config.RegisterAssemblyFromType<cm.frontend.core.Phone.Views.Behaviors.PhoneNumberValidator>();
            LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(ApplicationContext, config));  
#else
            LoadApplication(new cm.frontend.core.Phone.App());
#endif
        }
    }
}

