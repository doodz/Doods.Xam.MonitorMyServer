using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.iOS.Config;
using Foundation;
using System.IO;
using System.Xml;
using Google.MobileAds;
using UIKit;

namespace Doods.Xam.MonitorMyServer.iOS
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
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void OnCreate()
        {
            //TODO : SecureStorage Propriétés n'existent pas du côté iOS
            //SecureStorageImplementation.Password = FormatPassword();
            App.SetupContainer(Bootstrapper.CreateContainer());
            MobileAds.Configure("ca-app-pub-4922361220283829~3348751403");
            LoadConfiguration();
            LoadSettings();
        }

        private void LoadSettings()
        {
            //App.Container.Resolve<ISettings>().Initialize();
        }


        private string FormatPassword()
        {
            var bytes = new byte[] { 0xa3, 0x0f, 0x7f, 0xc1, 0x1c, 0x26, 0x38, 0x17, 0x7e, 0x8d };
            return bytes.ToString();
        }


        private void LoadConfiguration()
        {
            
            InitializeHockeyApp();
        }

        private void InitializeHockeyApp()
        {
            //var config = GetConfiguration();
            //if (string.IsNullOrEmpty(config.HockeyAppKey))
            //    return;

            //var manager = BITHockeyManager.SharedHockeyManager;
            //manager.Configure(config.HockeyAppKey);
            //manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
            //manager.StartManager();
            //manager.Authenticator.AuthenticateInstallation();
        }

        private IConfiguration GetConfiguration()
        {
            using (var stream = new StreamReader(NSBundle.MainBundle.PathForResource("App", "config")))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    var configService = App.Container.Resolve<IConfiguration>();
                    configService.LoadConfiguration(reader);
                    return configService;
                }
            }
        }
    }
}
