using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Servicies;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Login;
using System;
using Microsoft.AppCenter;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Mobile.Std.Config;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Doods.Xam.MonitorMyServer.Views.Processes2;
using Doods.Xam.MonitorMyServer.Views.Tests;
using MarcTron.Plugin;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Doods.Xam.MonitorMyServer
{
    public partial class App : Application
    {
        private static IContainer _container;

        private ILogger _logger;

        public App()
        {
            InitializeComponent();

            //var navigationService = Container.Resolve<INavigationService>();
            var navigationService = Container.ResolveKeyed<INavigationService>(NavigationServiceType);
            navigationService.Configure(nameof(MonitorMyServer.MainPage), typeof(MainPage));
            navigationService.Configure(nameof(LogInPage), typeof(LogInPage));
            navigationService.Configure(nameof(HostManagerPage), typeof(HostManagerPage));
            navigationService.Configure(nameof(EnumerateAllServicesFromAllHostsPage), typeof(EnumerateAllServicesFromAllHostsPage));
            navigationService.Configure(nameof(AptUpdatesPage), typeof(AptUpdatesPage));
            navigationService.Configure(nameof(AppShell), typeof(AppShell));
            navigationService.Configure(nameof(AddCustomCommandPage), typeof(AddCustomCommandPage));
            navigationService.Configure(nameof(CustomCommandListPage), typeof(CustomCommandListPage));
            navigationService.Configure(nameof(OpenmediavaultDashboardPage), typeof(OpenmediavaultDashboardPage));
            navigationService.Configure(nameof(ProcessesPage), typeof(ProcessesPage));
            navigationService.Configure(nameof(OpenmediavaultSettingsPage), typeof(OpenmediavaultSettingsPage));
            navigationService.Configure(nameof(OpenmediavaultFileSystemsPage), typeof(OpenmediavaultFileSystemsPage));
            navigationService.Configure(nameof(OpenmediavaultAddFileSystemsPage), typeof(OpenmediavaultAddFileSystemsPage));
            navigationService.Configure(nameof(OpenmediavaultPluginsPage), typeof(OpenmediavaultPluginsPage));
            navigationService.Configure(nameof(TestPage), typeof(TestPage));

            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(MonitorMyServer.MainPage));
            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(AppShell));
            //MainPage = mainPage;
            //MainPage = new AppShell();
            MainPage = new MyCustomShellApp();
        }

        public static NavigationServiceType NavigationServiceType = NavigationServiceType.ShellNavigation;
        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    throw new Exception("Please initialize the container first, through SetupContainer");

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
            builder.RegisterModule<Framework.Mobile.Std.Config.Bootstrapper>();
            builder.RegisterModule<Framework.Repository.Std.Config.Bootstrapper>();
            builder.RegisterModule<Views.Bootstrapper>();
            builder.RegisterModule<Services.Bootstrapper>();

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
            var config = _container.Resolve<IConfiguration>();
            if (CrossMTAdmob.IsSupported)
                CrossMTAdmob.Current.AdsId = config.AdsKey;
            // Handle when your app starts
            var key = Container.Resolve<IConfiguration>().MobileCenterKey;
            if (!string.IsNullOrEmpty(key))
            {
                Microsoft.AppCenter.AppCenter.Start(key, typeof(Analytics), typeof(Crashes));
            }

            //ConnctionService
            var connctionService = App.Container.Resolve<ConnctionService>();
            await connctionService.Init().ConfigureAwait(false);
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
           await Task.FromResult(0);
        }

        protected override async void OnResume()
        {
           
           
            await ProveYouHaveFingers();
            await Task.FromResult(0);
        }

        private static int _count;
        private async Task ProveYouHaveFingers()
        {

            var useFingerprint = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            if (!useFingerprint)
                return; 
          
            
                var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!"));
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