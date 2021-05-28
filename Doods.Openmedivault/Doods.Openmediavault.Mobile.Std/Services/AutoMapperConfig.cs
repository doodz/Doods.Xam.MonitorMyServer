using Autofac;
using AutoMapper;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmediavault.Rpc.Std.Data.V4;

namespace Doods.Openmediavault.Mobile.Std.Services
{
    public class AutoMapperConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<CpuInfoBean, CpuInfo>());
        }
    }


    public class AutoMapperMobileSshProfile : Profile
    {
        public AutoMapperMobileSshProfile()

        {
            CreateMap<OmvPlugins, PluginInfo>();
        }

        public override string ProfileName => GetType().ToString();
    }
}