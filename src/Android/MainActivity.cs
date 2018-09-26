
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using System;

namespace Doods.Xam.MonitorMyServer.Droid
{
    [Activity(Label = "Doods.Xam.MonitorMyServer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = true,
                VerbosePerformanceLogging = true,
                VerboseMemoryCacheLogging = true,
                VerboseLoadingCancelledLogging = true,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }
    }
}

