using Autofac;
using AutoMapper;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmedivault.Ssh.Std.Data;

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