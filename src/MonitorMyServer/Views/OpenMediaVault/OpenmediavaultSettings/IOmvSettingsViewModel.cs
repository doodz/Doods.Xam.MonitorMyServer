using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Openmediavault.Rpc.Std.Data.V4;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public interface IOmvSettingsViewModel<T> where T : OmvObject
    {
        T Settings { get; }
        ICommand SaveSettingsCmd { get; }
        ICommand ResetSettingsCmd { get; }
        Task ResetSettings();
        Task<bool> SaveSettings();
        Task<T> GetSettings();
    }
}