using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public interface IOmvSettingsViewModel<T> where T: OmvObject
    {
        T Settings { get; }
        ICommand SaveSettingsCmd { get; }
        ICommand ResetSettingsCmd { get; }
        Task ResetSettings();
        Task<bool> SaveSettings();
        Task<T> GetSettings();
    }
}