using Autofac;
using Doods.Xam.MonitorMyServer.Views.About;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Login;

namespace Doods.Xam.MonitorMyServer.Views
{
    internal class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterType<LoginPageViewModel>().AsSelf();
            builder.RegisterType<HostManagerPageViewModel>().AsSelf();
            builder.RegisterType<EnumerateAllServicesFromAllHostsViewModel>().AsSelf();
            builder.RegisterType<AptUpdatesPageViewModel>().AsSelf();
            builder.RegisterType<AppShellViewModel>().AsSelf();
            builder.RegisterType<AboutPageViewModel>().AsSelf();
            builder.RegisterType<AddCustomCommandPageViewModel>().AsSelf();
            builder.RegisterType<CustomCommandListPageViewModel>().AsSelf();
        }
    }
}