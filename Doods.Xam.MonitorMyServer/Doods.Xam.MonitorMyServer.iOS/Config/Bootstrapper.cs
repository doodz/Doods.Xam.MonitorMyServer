using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.iOS.Services;

namespace Doods.Xam.MonitorMyServer.iOS.Config
{
    public class Bootstrapper : Module
    {
        public static ContainerBuilder CreateContainer()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();


            return builder;
        }
    }
}