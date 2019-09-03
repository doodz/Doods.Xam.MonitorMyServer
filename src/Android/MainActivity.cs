
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using System;
using Android.Content;
using Android.Gms.Ads;
using Android.Net.Wifi;
using Android.Runtime;
using Plugin.Fingerprint;

namespace Doods.Xam.MonitorMyServer.Droid
{
    [Activity(Label = "Doods.Xam.MonitorMyServer", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-4922361220283829~5150956035");
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = true,
                VerbosePerformanceLogging = true,
                VerboseMemoryCacheLogging = true,
                VerboseLoadingCancelledLogging = true,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);


            wifi = (WifiManager)ApplicationContext.GetSystemService(Context.WifiService);
            mlock = wifi.CreateMulticastLock("Zeroconf lock");
            mlock.Acquire();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        WifiManager wifi;
        WifiManager.MulticastLock mlock;
        protected override void OnDestroy()
        {
            if (mlock != null)
            {
                mlock.Release();
                mlock = null;
            }
            base.OnDestroy();
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

