using Autofac;
using Doods.Framework.Std;
using Java.Util.Logging;

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