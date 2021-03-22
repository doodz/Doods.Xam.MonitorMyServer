using System;
using Autofac;
using AutoMapper;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Data.Nas;
using Zeroconf;
using Disk = Doods.Synology.Webapi.Std.Datas.Disk;

namespace Doods.Xam.MonitorMyServer.Services
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     https://github.com/AutoMapper/AutoMapper/issues/2505
    /// </remarks>
    public enum MyEnumeration
    {
        tricks,
        forAutomapper,
        seeAutomapperIssue
    }

    internal class AutoMapperConfig : Module
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

            var assembliesToScane =
                AppDomain.CurrentDomain.GetAssemblies(); //AutoMapperMobileSshProfile;ZeroconfHostProfile

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
            var map = CreateMap<Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder, SharedFolder>();
            map.ConvertUsing(new OpenmediavaultShareConverteur());
            var map2 = CreateMap<Share, SharedFolder>();
            map2.ConvertUsing(new SynologyShareConverteur());

            var map3 = CreateMap<Openmediavault.Rpc.std.Data.V4.FileSystem.Disk, Data.Nas.Disk>();
            map3.ConvertUsing(new OpenmediavaultDiskConverteur());
            var map4 = CreateMap<Disk, Data.Nas.Disk>();
            map4.ConvertUsing(new SynologyDiskConverteur());


            var map5 = CreateMap<Disk, FileSystem>();
            map5.ConvertUsing(new SynologyFileSystemConverteur());

            var map6 = CreateMap<OmvFilesystems, FileSystem>();
            map6.ConvertUsing(new OpenmediavaultFileSystemConverteur());

            var map7 = CreateMap<Upgradable, Data.Nas.Package>();
            map7.ConvertUsing(new AptPackageConverteur());

            var map8 = CreateMap<Upgradable, Data.Nas.Package>();
            map8.ConvertUsing(new AptPackageConverteur());

            var map9 = CreateMap<Upgraded, Data.Nas.Package>();
            map9.ConvertUsing(new OpenmediavaultPackageConverteur());

            var map10 = CreateMap<Synology.Webapi.Std.Datas.Package, Data.Nas.Package>();
            map10.ConvertUsing(new SynologyPackageConverteur());
        }

        public override string ProfileName => GetType().ToString();
    }

    public class SynologyShareConverteur : ITypeConverter<Share, SharedFolder>
    {
        public SharedFolder Convert(Share source, SharedFolder destination, ResolutionContext context)
        {
            var test = new SharedFolder
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

    public class
        OpenmediavaultShareConverteur : ITypeConverter<Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder,
            SharedFolder>
    {
        public SharedFolder Convert(Openmediavault.Rpc.Std.Data.V4.SharedFolders.SharedFolder source,
            SharedFolder destination, ResolutionContext context)
        {
            var test = new SharedFolder
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

    public class SynologyDiskConverteur : ITypeConverter<Disk, Data.Nas.Disk>
    {
        public Data.Nas.Disk Convert(Disk source, Data.Nas.Disk destination, ResolutionContext context)
        {
            var test = new Data.Nas.Disk
            {
                TotalSize = source.SizeTotal,
                DeviceName = source.Id,
                Vendor = source.Vendor
            };
            return test;
        }
    }

    public class
        OpenmediavaultDiskConverteur : ITypeConverter<Openmediavault.Rpc.std.Data.V4.FileSystem.Disk, Data.Nas.Disk>
    {
        public Data.Nas.Disk Convert(Openmediavault.Rpc.std.Data.V4.FileSystem.Disk source, Data.Nas.Disk destination,
            ResolutionContext context)
        {
            var test = new Data.Nas.Disk
            {
                TotalSize = source.Size,
                DeviceName = source.Devicename,
                Vendor = source.Vendor
            };
            return test;
        }
    }

    public class SynologyFileSystemConverteur : ITypeConverter<Disk, FileSystem>
    {
        public FileSystem Convert(Disk source, FileSystem destination, ResolutionContext context)
        {
            var test = new FileSystem
            {
                Device = source.Device,
                Name = source.LongName,
                Size = source.SizeTotal
            };
            return test;
        }
    }

    public class OpenmediavaultFileSystemConverteur : ITypeConverter<OmvFilesystems, FileSystem>
    {
        public FileSystem Convert(OmvFilesystems source, FileSystem destination, ResolutionContext context)
        {
            var test = new FileSystem
            {
                Device = source.Parentdevicefile,
                Name = string.IsNullOrWhiteSpace(source.Label) ? source.Parentdevicefile : source.Label,
                Size = source.Size
            };
            return test;
        }
    }
    
    public class AptPackageConverteur : ITypeConverter<Upgradable, Data.Nas.Package>
    {
        public Data.Nas.Package Convert(Upgradable source, Data.Nas.Package destination, ResolutionContext context)
        {
            var test = new Data.Nas.Package
            {
                Name = source.Name,
                Desc = source.ShowFormatedInfo,
                Source = source.FromRepo,
                Status = source.NewVersion
            };
            return test;
        }
    }
    public class OpenmediavaultPackageConverteur : ITypeConverter<Upgraded, Data.Nas.Package>
    {
        public Data.Nas.Package Convert(Upgraded source, Data.Nas.Package destination, ResolutionContext context)
        {
            var test = new Data.Nas.Package
            {
                Name = source.Name,
                Desc = source.Description,
                Source = source.Source,
                Status = source.Package
            };
            return test;
        }
    }


    public class SynologyPackageConverteur : ITypeConverter<Synology.Webapi.Std.Datas.Package, Data.Nas.Package>
    {
        public Data.Nas.Package Convert(Synology.Webapi.Std.Datas.Package source, Data.Nas.Package destination, ResolutionContext context)
        {
            var test = new Data.Nas.Package
            {
              
                Name = source.Name,
                
            };
            return test;
        }
    }
}