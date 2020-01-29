using System;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Std;
using Doods.Framework.Std.Services;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public abstract class BaseSettingsViewModel<T> : ViewModel,IOmvSettingsViewModel<T> where T : OmvObject
    {

        protected readonly IRpcService SshService = App.Container.Resolve<IRpcService>();

        public ITranslateService TranslateService { get; } =
            new TranslateService(new ResourceManager("Doods.Openmediavault.Mobile.Std.Resources.openmediavault",
                typeof(openmediavault).Assembly));
        public T Settings { get; protected set; }
        public ICommand SaveSettingsCmd => new Command(async () => await SaveSettings());

        public ICommand ResetSettingsCmd => new Command(async () => await GetSettings());

        public Task ResetSettings()
        {
            throw new NotImplementedException();
        }

        public abstract Task<bool> SaveSettings();


        public abstract Task<T> GetSettings();
      
    }
}