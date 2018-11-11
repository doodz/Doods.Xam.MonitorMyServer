using Autofac;

namespace Doods.Xam.MonitorMyServer.Services
{
    class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();
            builder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            
            
        }
    }
}
