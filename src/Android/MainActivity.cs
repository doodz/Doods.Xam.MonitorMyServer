using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Net.Wifi;
using Android.Runtime;
using Plugin.Fingerprint;

namespace Doods.Xam.MonitorMyServer.Droid
{
    [Activity(Label = "Local_Testing", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            _wifi = (WifiManager)ApplicationContext.GetSystemService(Context.WifiService);
            _mlock = _wifi.CreateMulticastLock("Zeroconf lock");
            _mlock.Acquire();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
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


       
    }
}

