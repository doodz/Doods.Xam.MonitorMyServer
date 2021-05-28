using Autofac;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Synology.Webapi.Std;

namespace Doods.Xam.MonitorMyServer.Services
{
    internal class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HistoryService>().As<IHistoryService>().SingleInstance();

            builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();

            builder.RegisterType<ConnctionService>().SingleInstance().AsSelf();
            builder.RegisterType<OmvService>().As<IRpcService>()
                .As<IOMVSshBackgroundService>().SingleInstance();
            builder.RegisterType<RewardService>().As<IRewardService>().SingleInstance();

            builder.RegisterType<SshService>().AsSelf();
            builder.RegisterType<OmvRpcService>().AsSelf();
            //builder.RegisterType<OmvSshService>().AsSelf();//refact

            builder.RegisterType<SshServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<OmvServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<SynoServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<WebminServiceProvider>().SingleInstance().AsSelf();
            //builder.RegisterType<WebminCgiService>().SingleInstance().AsSelf();


            builder.Register(c => c.Resolve<WebminServiceProvider>().Value).As<IWebminCgiService>();
            builder.Register(c => c.Resolve<SshServiceProvider>().Value).As<ISshService>();
            builder.Register(c => c.Resolve<OmvServiceProvider>().Value).As<IOmvService>();
            builder.Register(c => c.Resolve<SynoServiceProvider>().Value).As<ISynologyCgiService>();
            builder.Register(c => c.Resolve<ConnctionService>().GetNasService()).As<INasService>();
            builder.Register(c => c.Resolve<ConnctionService>().GetPackageService()).As<IPackageUpdates>();
            //builder.Register(c => c.Resolve<SshServiceProvider>().Value).ExternallyOwned().Keyed<ISshService>("1");
            //builder.Register(c => c.Resolve<OmvServiceProvider>().Value).ExternallyOwned().Keyed<IOmvService>("1");
        }
    }
}