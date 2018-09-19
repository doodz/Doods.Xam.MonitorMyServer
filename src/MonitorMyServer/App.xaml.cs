using Autofac;
using Doods.Framework.Std;
using System;
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

            MainPage = new MainPage();
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