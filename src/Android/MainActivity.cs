using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using System;
using Android.Content;
using Android.Net.Wifi;
using Android.Runtime;
using Plugin.Fingerprint;
using Xamarin.Forms.PancakeView.Droid;

namespace Doods.Xam.MonitorMyServer.Droid
{
    [Activity(Label = "Local_Testing", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
            PancakeViewRenderer.Init();

            _wifi = (WifiManager)ApplicationContext.GetSystemService(Context.WifiService);
            _mlock = _wifi.CreateMulticastLock("Zeroconf lock");
            _mlock.Acquire();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private WifiManager _wifi;
        private WifiManager.MulticastLock _mlock;
        protected override void OnDestroy()
        {
            //base.OnDestroy();
            if (_mlock != null)
            {
                _mlock.Release();
                _mlock = null;
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

