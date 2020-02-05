using System;
using Autofac;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Doods.Openmedivault.Ssh.Std;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Xam.MonitorMyServer.Services
{
    internal class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataProvider>().As<IDataProvider>().SingleInstance();
           
            builder.RegisterType<ConnctionService>().AsSelf();
            builder.RegisterType<OmvService>().As<IRpcService>()
                .As<IOMVSshBackgroundService>().SingleInstance();
            builder.RegisterType<RewardService>().As<IRewardService>().SingleInstance();

            builder.RegisterType<SshService>().AsSelf();
            builder.RegisterType<OmvRpcService>().AsSelf();
            builder.RegisterType<OmvSshService>().AsSelf();
            

            builder.RegisterType<OmvServiceProvider>().SingleInstance().AsSelf();
            builder.RegisterType<SshServiceProvider>().SingleInstance().AsSelf();
            builder.Register(c => c.Resolve<SshServiceProvider>().Value).As<ISshService>();
            builder.Register(c => c.Resolve<OmvServiceProvider>().Value).As<IOmvService>();

            //builder.Register(c => c.Resolve<SshServiceProvider>().Value).ExternallyOwned().Keyed<ISshService>("1");
            //builder.Register(c => c.Resolve<OmvServiceProvider>().Value).ExternallyOwned().Keyed<IOmvService>("1");
            
        }
    }

    public class SshServiceProvider
    {

        public SshServiceProvider(SshService service)
        {
           
            Value = service;
        }
        //private volatile ISshService _value;

        public ISshService Value { get; set; }

        public void ChangeValue(ISshService value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value == Value) return;

            var oldValue = Value;
            Value = value;
            // oldValue.Dispose();
        }

        public void Dispose()
        {
            //this._value.Dispose();
        }
    }

    public class OmvServiceProvider
    {

        public OmvServiceProvider()
        {
            Value = new OmvRpcService(null,null,null);
        }
        //private volatile IOmvService _value;

        public IOmvService  Value { get; set; }

    public void ChangeValue(IOmvService value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value == Value) return;

            var oldValue = Value;
            Value = value;
            // oldValue.Dispose();
        }

        public void Dispose()
        {
            //this._value.Dispose();
        }
    }
}