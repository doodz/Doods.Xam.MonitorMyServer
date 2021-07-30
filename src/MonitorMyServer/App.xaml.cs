using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Http.Std.Ping;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Helpers;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Resx.Cockpit;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Linux.DisksUsage;
using Doods.Xam.MonitorMyServer.Views.Linux.Logs;
using Doods.Xam.MonitorMyServer.Views.Login;
using Doods.Xam.MonitorMyServer.Views.NAS.PackageUpdates;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates;
using Doods.Xam.MonitorMyServer.Views.Processes2;
using Doods.Xam.MonitorMyServer.Views.SelectService;
using Doods.Xam.MonitorMyServer.Views.Synology.SynoStorage;
using Doods.Xam.MonitorMyServer.Views.SynologyInfo;
using Doods.Xam.MonitorMyServer.Views.Tests;
using Doods.Xam.MonitorMyServer.Views.Webmin.States;
using MarcTron.Plugin;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bootstrapper = Doods.Framework.Mobile.Std.Config.Bootstrapper;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Doods.Xam.MonitorMyServer
{
    public partial class App : Application
    {
        private static IContainer _container;

        public static NavigationServiceType NavigationServiceType = NavigationServiceType.ShellNavigation;

        private static int _count;

        private ILogger _logger;

        public App()
        {
            
            InitializeComponent();
            LocalizationResourceManager.Current.Init(cockpit.ResourceManager);


            //var navigationService = Container.Resolve<INavigationService>();
            var navigationService = Container.ResolveKeyed<INavigationService>(NavigationServiceType);
            navigationService.Configure(nameof(MonitorMyServer.MainPage), typeof(MainPage));
            navigationService.Configure(nameof(LogInPage), typeof(LogInPage));
            navigationService.Configure(nameof(HostManagerPage), typeof(HostManagerPage));
            navigationService.Configure(nameof(EnumerateAllServicesFromAllHostsPage),
                typeof(EnumerateAllServicesFromAllHostsPage));
            navigationService.Configure(nameof(SelectSupportedServiciePage), typeof(SelectSupportedServiciePage));
            navigationService.Configure(nameof(AptUpdatesPage), typeof(AptUpdatesPage));
            navigationService.Configure(nameof(AppShell), typeof(AppShell));
            navigationService.Configure(nameof(AddCustomCommandPage), typeof(AddCustomCommandPage));
            navigationService.Configure(nameof(CustomCommandListPage), typeof(CustomCommandListPage));
            navigationService.Configure(nameof(ProcessesPage), typeof(ProcessesPage));
            navigationService.Configure(nameof(TestPage), typeof(TestPage));
            navigationService.Configure(nameof(DisksUsagePage), typeof(DisksUsagePage));
            navigationService.Configure(nameof(LogsPage), typeof(LogsPage));

            navigationService.Configure(nameof(OpenmediavaultDashboardPage), typeof(OpenmediavaultDashboardPage));
            navigationService.Configure(nameof(OpenmediavaultSettingsPage), typeof(OpenmediavaultSettingsPage));
            navigationService.Configure(nameof(OpenmediavaultFileSystemsPage), typeof(OpenmediavaultFileSystemsPage));
            navigationService.Configure(nameof(OpenmediavaultAddFileSystemsPage),
                typeof(OpenmediavaultAddFileSystemsPage));
            navigationService.Configure(nameof(OpenmediavaultPluginsPage), typeof(OpenmediavaultPluginsPage));
            navigationService.Configure(nameof(OpenmediavaultUpdatesPage), typeof(OpenmediavaultUpdatesPage));

            navigationService.Configure(nameof(SynologyInfoPage), typeof(SynologyInfoPage));
            navigationService.Configure(nameof(SynologyStoragePage), typeof(SynologyStoragePage));

            navigationService.Configure(nameof(WebminStatsPage), typeof(WebminStatsPage));

            navigationService.Configure(nameof(PackageUpdatesPage), typeof(PackageUpdatesPage));
            

            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(MonitorMyServer.MainPage));
            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(AppShell));
            //MainPage = mainPage;
            //MainPage = new AppShell();

            MainPage = new MyCustomShellApp();
        }

        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    throw new Exception("Please initialize the container first, through SetupContainer" +
                                        $"{Environment.NewLine}{Environment.StackTrace}");


                return _container;
            }
        }

        protected ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Container.Resolve<ILogger>();

                return _logger;
            }
        }

        public static void SetupContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<Bootstrapper>();
            builder.RegisterModule<Framework.Repository.Std.Config.Bootstrapper>();
            builder.RegisterModule<Openmediavault.Mobile.Std.Config.Bootstrapper>();
            builder.RegisterModule<Views.Bootstrapper>();
            builder.RegisterModule<Services.Bootstrapper>();

            builder.RegisterType<SynchronizedCache<object>>().As<ISynchronizedCache<object>>().SingleInstance();
            builder.RegisterType<AddressLookupService>().As<IAddressLookupService>().SingleInstance();
            builder.RegisterType<IcmpPingService>().AsSelf();
            builder.RegisterType<RdpPortPingService>().AsSelf();
            builder.RegisterType<PingService>().As<IPingService>().SingleInstance();


            //builder.RegisterModule<Services.AutoMapperConfig>();

            builder.RegisterModule<AutoMapperConfig>();

            ////https://github.com/marcojak/TestMTAdmob

            ////You can add here the id of your test devices!
            ////CrossMTAdmob.Current.TestDevices = new List<string>() {  };

            //var assembliesToScane = AppDomain.CurrentDomain.GetAssemblies();
            ////var allTypes = assembliesToScane.SelectMany(a => a.ExportedTypes).ToArray();

            ////var profiles =
            ////    allTypes
            ////        .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
            ////        .Where(t => !t.GetTypeInfo().IsAbstract);


            //var configuration = new MapperConfiguration(cfg => cfg.AddMaps(assembliesToScane));
            //configuration.CompileMappings();
            ////Mapper.Initialize(cfg =>
            ////{
            ////    foreach (var profile in profiles)
            ////    {
            ////        cfg.AddProfile(profile);
            ////    }
            ////});
            //var mapper = configuration.CreateMapper();

            _container = builder.Build();
        }

        protected override async void OnStart()
        {
            await ProveYouHaveFingers();
            base.OnStart();
            ThemeHelper.ChangeTheme(ThemeHelper.CurrentTheme, true);

            var config = _container.Resolve<IConfiguration>();
            if (CrossMTAdmob.IsSupported)
                CrossMTAdmob.Current.AdsId = config.AdsKey;
            // Handle when your app starts
            var key = Container.Resolve<IConfiguration>().MobileCenterKey;
            if (!string.IsNullOrEmpty(key))
                Microsoft.AppCenter.AppCenter.Start(key, typeof(Analytics), typeof(Crashes));

            //ConnctionService
            var connctionService = Container.Resolve<IConnctionService>();
            await connctionService.Init().ConfigureAwait(false);
        }

        protected override async void OnSleep()
        {
            base.OnSleep();
            // Handle when your app sleeps
            await Task.FromResult(0);
        }

        protected override async void OnResume()
        {
            await ProveYouHaveFingers();

            base.OnResume();
            ThemeHelper.ChangeTheme(ThemeHelper.CurrentTheme, true);
            await Task.FromResult(0);
        }

        private async Task ProveYouHaveFingers()
        {
            var useFingerprint = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            if (!useFingerprint)
                return;


            var result =
                await CrossFingerprint.Current.AuthenticateAsync(
                    new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!"));
            if (!result.Authenticated)
            {
                _count++;
                if (_count <= 5)
                    await ProveYouHaveFingers();
                Thread.CurrentThread.Abort();
            }

            _count = 0;
            //CrossFingerprint.Current.

            //var result = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!");
            //if (result.Authenticated)
            //{
            //    // do secret stuff :)
            //}
            //else
            //{
            //    await ProveYouHaveFingers();
            //}
            await Task.FromResult(0);
        }
    }
}