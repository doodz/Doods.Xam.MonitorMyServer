using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Droid.Services;

namespace Doods.Xam.MonitorMyServer.Droid.Config
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