using Autofac;
using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;

namespace Doods.Framework.Repository.Std.Config
{
    public class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            builder.RegisterType<RepositoryBase>().As<IRepository>();
            builder.RegisterType<RepositoryCache>().As<IRepositoryCache>();
        }
    }
}