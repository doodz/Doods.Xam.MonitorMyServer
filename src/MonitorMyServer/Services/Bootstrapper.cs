using Autofac;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Doods.Openmedivault.Ssh.Std;
using Doods.Openmedivault.Ssh.Std.Requests;
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
            builder.RegisterType<OmvSshService>().AsSelf();
            builder.RegisterType<OmvSshService>().AsSelf();

            builder.RegisterType<OmvServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<SshServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<SynoServiceProvider>().SingleInstance().AsSelf();
            builder.Register(c => c.Resolve<SshServiceProvider>().Value).As<ISshService>();
            builder.Register(c => c.Resolve<OmvServiceProvider>().Value).As<IOmvService>();
            builder.Register(c => c.Resolve<SynoServiceProvider>().Value).As<ISynologyCgiService>();
            
            //builder.Register(c => c.Resolve<SshServiceProvider>().Value).ExternallyOwned().Keyed<ISshService>("1");
            //builder.Register(c => c.Resolve<OmvServiceProvider>().Value).ExternallyOwned().Keyed<IOmvService>("1");

        }
    }
}