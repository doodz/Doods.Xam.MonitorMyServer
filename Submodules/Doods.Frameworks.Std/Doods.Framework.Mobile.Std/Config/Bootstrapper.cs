using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Servicies;
using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Std.Config
{
    public enum NavigationServiceType
    {
        ViewNavigation,
        ShellNavigation
    }


    public class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyDeviceInfo>().As<IDeviceInfo>().SingleInstance();
            builder.RegisterType<Configuration>().As<IConfiguration>().SingleInstance();
            builder.RegisterType<TelemetryService>().As<ITelemetryService>().SingleInstance();
            builder.RegisterType<ViewNavigationService>()
                .Keyed<INavigationService>(NavigationServiceType.ViewNavigation).SingleInstance();
            builder.RegisterType<ShellNavigationService>()
                .Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance();
            builder.RegisterType<MessageBoxService>().As<IMessageBoxService>().SingleInstance();
        }
    }
}