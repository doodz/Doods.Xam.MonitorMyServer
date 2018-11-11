using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Interfaces;
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
            builder.RegisterType<SQLiteFactory>().As<ISqLiteFactory>().SingleInstance();
            builder.RegisterType<FileHelper>().As<IFileHelper>().SingleInstance();

            return builder;
        }
    }
}