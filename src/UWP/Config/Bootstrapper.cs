using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Interfaces;
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
            builder.RegisterType<SQLiteFactory>().As<ISqLiteFactory>().SingleInstance();
            builder.RegisterType<FileHelper>().As<IFileHelper>().SingleInstance();

            return builder;
        }
    }
}
