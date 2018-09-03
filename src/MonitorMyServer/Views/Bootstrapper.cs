using Autofac;

namespace Doods.Xam.MonitorMyServer.Views
{
    class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MainPageViewModel>().AsSelf();
        }
    }
}