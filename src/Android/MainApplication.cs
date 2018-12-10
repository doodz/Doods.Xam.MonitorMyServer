using Android;
using Android.App;
using Android.Runtime;
using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Droid.Config;
using System;
using System.Xml;

[assembly: UsesPermission(Manifest.Permission.Internet)]
[assembly: UsesPermission(Manifest.Permission.Camera)]
[assembly: UsesPermission(Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]
[assembly: UsesPermission(Manifest.Permission.ChangeWifiMulticastState)]
namespace Doods.Xam.MonitorMyServer.Droid
{
#if DEBUG
    [Application(Theme = "@style/MainTheme", Debuggable = true)]
#else
    [Application(Theme = "@style/MainTheme", Debuggable = false)]
#endif
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaRef, JniHandleOwnership transfert) : base(javaRef, transfert)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
            //SecureStorageImplementation.StoragePassword = FormatPassword();
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            App.SetupContainer(Bootstrapper.CreateContainer());
            LoadConfiguration();
            LoadSettings();
        }

        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
            App.Container.Resolve<ILogger>().Error(e.Exception);
            //App.Container.Resolve<ITelemetryService>().Exception(new UnHandledException(e.Exception));
        }

        private void LoadSettings()
        {
            //App.Container.Resolve<ISettings>().Initialize();
        }

        private string FormatPassword()
        {
            var bytes = new byte[] {0xa3, 0x0f, 0x7f, 0xc1, 0x1c, 0x26, 0x38, 0x17, 0x7e, 0x8d};
            return bytes.ToString();
        }

        private void LoadConfiguration()
        {
            LoadDoodsConfiguration();
        }

        private void LoadDoodsConfiguration()
        {
            var fileName = "App.config";

#if DEBUG
            fileName = "Debug.config";
#endif

            using (var stream = Assets.Open(fileName))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    var configService = App.Container.Resolve<IConfiguration>();
                    configService.LoadConfiguration(reader);
                }
            }
        }
    }
}