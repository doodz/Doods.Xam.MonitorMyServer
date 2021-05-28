using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Interfaces
{
    public interface IOMVSettingsService
    {
        Task<PowerManagementSetting> GetPowerManagementSetting();
        Task<NetworkSetting> GetNetworkSetting();
        Task<TimeSetting> GetDateAndTimeSetting();
        Task<IEnumerable<string>> GetTimeZoneList();
        Task<AptSetting> GetAptSettings();

        Task<WebGuiSetting> GetWebGuiSettings();

        Task<bool> SetPowerManagementSetting(PowerManagementSetting powerManagementSetting);
        Task<bool> SetNetworkSetting(NetworkSetting networkSetting);
        Task<bool> SetDateAndTimeSetting(TimeSetting timeSetting);

        Task<bool> SetAptSettings(AptSetting aptSetting);
        Task<bool> SetWebGuiSettings(WebGuiSetting webGuiSetting);
    }
}