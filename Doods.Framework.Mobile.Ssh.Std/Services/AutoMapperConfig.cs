using Autofac;
using AutoMapper;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Queries;

namespace Doods.Framework.Mobile.Ssh.Std.Services
{
    public class AutoMapperConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CpuInfoBean, CpuInfo>());
        }
    }


    public class AutoMapperMobileSshProfile : Profile
    {
        public AutoMapperMobileSshProfile()

        {
            CreateMap<CpuInfoBean, CpuInfo>();
            CreateMap<DiskUsageBean, DiskUsage>();
            CreateMap<UpgradableBean, Upgradable>();
        }

        public override string ProfileName => GetType().ToString();
    }
}