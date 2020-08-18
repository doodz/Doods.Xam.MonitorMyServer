using Android;
using Android.App;
using Android.Runtime;
using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Droid.Config;
using System;
using System.Threading.Tasks;
using System.Xml;
using Android.Gms.Ads;
using Doods.Xam.MonitorMyServer.Droid.Services;
using FFImageLoading;
using Xamarin.Forms.PancakeView.Droid;

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
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
        }

        public override void OnCreate()
        {
            base.OnCreate();

           

            
            Task.Run( () => {  MyMethod(); });
            //SecureStorageImplementation.StoragePassword = FormatPassword();
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            App.SetupContainer(Bootstrapper.CreateContainer());
            LoadConfiguration();

            LoadSettings();
        }

        private void MyMethod()
        {
            Xamarin.Essentials.Platform.Init(this);
            Xamarin.Forms.Svg.Droid.SvgImage.Init(this); //need to write here
            Rg.Plugins.Popup.Popup.Init(this, null);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = true,
                VerbosePerformanceLogging = true,
                VerboseMemoryCacheLogging = true,
                VerboseLoadingCancelledLogging = true,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            PancakeViewRenderer.Init();
        }
       
        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;

            try
            {
                App.Container.Resolve<ILogger>().Error(e.Exception);
            }
            catch (Exception ex2)
            {
                new LocalLogger().Error(e.Exception);
                throw ex2;
            }
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
                    MobileAds.Initialize(ApplicationContext, configService.AppAdsKey);//
                }
            }
           
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