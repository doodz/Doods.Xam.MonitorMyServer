using Android.App;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace Doods.Xam.MonitorMyServer.Droid
{
    [Activity(Label = "Local_Testing", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private WifiManager.MulticastLock _mlock;

        private WifiManager _wifi;

        protected override void OnCreate(Bundle bundle)
        {
            _wifi = (WifiManager) ApplicationContext.GetSystemService(WifiService);
            _mlock = _wifi.CreateMulticastLock("Zeroconf lock");
            _mlock.Acquire();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

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