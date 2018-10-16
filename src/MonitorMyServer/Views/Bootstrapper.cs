using Autofac;
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
        }
    }
}