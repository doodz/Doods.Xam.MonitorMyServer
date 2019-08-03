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
using AutoMapper;
using Doods.Framework.Mobile.Std.Config;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using MarcTron.Plugin;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(MonitorMyServer.MainPage));
            //var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(AppShell));
            //MainPage = mainPage;
            MainPage = new AppShell();
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
            //builder.RegisterModule<Framework.Mobile.Ssh.Std.Services.AutoMapperConfig>();

            //https://github.com/marcojak/TestMTAdmob
           
            //You can add here the id of your test devices!
            //CrossMTAdmob.Current.TestDevices = new List<string>() {  };

            var assembliesToScane = AppDomain.CurrentDomain.GetAssemblies();
            var allTypes = assembliesToScane.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles =
                allTypes
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            _container = builder.Build();

            

        }

        protected override void OnStart()
        {
            var config = _container.Resolve<IConfiguration>();
            CrossMTAdmob.Current.AdsId = config.AdsKey;
            // Handle when your app starts
            var key = Container.Resolve<IConfiguration>().MobileCenterKey;
            if (!string.IsNullOrEmpty(key))
            {
                Microsoft.AppCenter.AppCenter.Start(key, typeof(Analytics), typeof(Crashes));

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}