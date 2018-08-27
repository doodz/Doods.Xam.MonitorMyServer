using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.UWP.Services;

namespace Doods.Xam.MonitorMyServer.UWP.Config
{
    internal class Bootstrapper
    {
        internal static ContainerBuilder CreateContainer()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();


            return builder;
        }
    }
}
