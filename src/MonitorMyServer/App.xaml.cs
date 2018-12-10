using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Servicies;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Login;
using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
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

            var navigationService = Container.Resolve<INavigationService>();
            navigationService.Configure(nameof(MonitorMyServer.MainPage), typeof(MainPage));
            navigationService.Configure(nameof(LogInPage), typeof(LogInPage));
            navigationService.Configure(nameof(HostManagerPage), typeof(HostManagerPage));
            navigationService.Configure(nameof(EnumerateAllServicesFromAllHostsPage), typeof(EnumerateAllServicesFromAllHostsPage));
            
            var mainPage = ((ViewNavigationService)navigationService).SetRootPage(nameof(MainPage));
            MainPage = mainPage;
        }

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
            // Handle when your app starts
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