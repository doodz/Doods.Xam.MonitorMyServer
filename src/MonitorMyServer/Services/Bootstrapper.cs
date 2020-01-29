using Autofac;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Xam.MonitorMyServer.Services
{
    class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();
            //builder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            builder.RegisterType<OmvService>().As<IOmvService>().As<ISshService>().As<IRpcService>().As<IOMVSshBackgroundService>().SingleInstance();
            builder.RegisterType<RewardService>().As<IRewardService>().SingleInstance();
        }
    }
}
