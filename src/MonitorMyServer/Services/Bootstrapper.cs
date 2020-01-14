using Autofac;

namespace Doods.Xam.MonitorMyServer.Services
{
    class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();
            //builder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            builder.RegisterType<OMVSshService>().As<ISshService>().As<IOMVSshService>().As<IOMVSshBackgroundService>().SingleInstance();
            builder.RegisterType<RewardService>().As<IRewardService>().SingleInstance();
        }
    }
}
