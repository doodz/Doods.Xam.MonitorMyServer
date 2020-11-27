using System;
using System.Linq;
using Autofac;
using AutoMapper;
using Doods.Xam.MonitorMyServer.Data;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///https://github.com/AutoMapper/AutoMapper/issues/2505
    /// </remarks>
    public enum MyEnumeration
    {
        tricks,
        forAutomapper,
        seeAutomapperIssue
    }
    class AutoMapperConfig : Module
    {
        public static void RegisterMappings()
        {
            //AutoMapper.Mapper.
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();
            //builder.RegisterType<SshService>().As<ISshService>().SingleInstance();

           
           // AutoMapper.Mappers.EnumToEnumMapper.Map<MyEnumeration, MyEnumeration>(MyEnumeration.tricks);

            var assembliesToScane = AppDomain.CurrentDomain.GetAssemblies();//AutoMapperMobileSshProfile;ZeroconfHostProfile

            //assembliesToScane = assembliesToScane.Where(a => !a.FullName.Contains("Doods")).ToArray();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembliesToScane);
                //cfg.AddProfile<Doods.Framework.Mobile.Ssh.Std.Services.AutoMapperMobileSshProfile>();
                //cfg.AddProfile<Doods.Openmediavault.Mobile.Std.Services.AutoMapperMobileSshProfile>();
                cfg.AddProfile<ZeroconfHostProfile>();
                cfg.AddProfile<AutoMapperNasProfile>();
            });
            
            configuration.CompileMappings();
            var mapper = configuration.CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>();
         


            //Mapper.Initialize(cfg =>
            //    //cfg.CreateMap<IZeroconfHost, ZeroconfHost>()
            //    cfg.AddProfile<ZeroconfHostProfile>()
            //);

            //builder.Register(c => new MapperConfiguration(cfg => {
            //    foreach (var profile in c.Resolve<IEnumerable<Profile>>())
            //    {
            //        cfg.AddProfile(profile);
            //    }
            //})).AsSelf().SingleInstance();

            //builder.Register(c => c.Resolve<MapperConfiguration>()
            //        .CreateMapper(c.Resolve))
            //    .As<IMapper>()
            //    .InstancePerLifetimeScope();
        }
    }


    public class ZeroconfHostProfile : Profile
    {
        public ZeroconfHostProfile()
        {
            var map = CreateMap<IZeroconfHost, ZeroconfHost>();
            map.ConvertUsing(new ZeroconfHostConverteur());
        }
    }

    public class ZeroconfHostConverteur : ITypeConverter<IZeroconfHost, ZeroconfHost>
    {
        public ZeroconfHost Convert(IZeroconfHost source, ZeroconfHost destination, ResolutionContext context)
        {
            var test = new ZeroconfHost(source.IPAddresses, source.Services);
            test.Id = source.Id;
            test.DisplayName = source.DisplayName;
            test.IPAddress = source.IPAddress;
            return test;
        }
    }

    public class AutoMapperNasProfile : Profile
    {
        public AutoMapperNasProfile()

        {
            var map =CreateMap<Doods.Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder, Data.Nas.SharedFolder>();
            map.ConvertUsing(new OpenmediavaultShareConverteur());
            var map2 =CreateMap<Doods.Synology.Webapi.Std.Datas.Share, Data.Nas.SharedFolder>();
            map2.ConvertUsing(new SynologyShareConverteur());
        }

        public override string ProfileName => GetType().ToString();
    }
    public class SynologyShareConverteur : ITypeConverter<Doods.Synology.Webapi.Std.Datas.Share, Data.Nas.SharedFolder>
    {
        public Data.Nas.SharedFolder Convert(Doods.Synology.Webapi.Std.Datas.Share source, Data.Nas.SharedFolder destination, ResolutionContext context)
        {
            var test = new Data.Nas.SharedFolder
            {
                Name = source.Name,
                Description = source.Desc,
                Volume = source.VolPath,
                Type = "btrfs",
                Uuid = source.Uuid
            };
            return test;
        }
    }

    public class OpenmediavaultShareConverteur : ITypeConverter<Doods.Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder, Data.Nas.SharedFolder>
    {
        public Data.Nas.SharedFolder Convert(Doods.Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder source, Data.Nas.SharedFolder destination, ResolutionContext context)
        {
            var test = new Data.Nas.SharedFolder
            {
                Name = source.Name,
                Description = source.Comment,
                Volume = source.Device,
                Type = source.Mntent?.Type,
                Uuid = source.Uuid
            };
            return test;
        }
    }
}