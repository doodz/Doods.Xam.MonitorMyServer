using System.Threading.Tasks;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface ISettingsBase
    {
        bool TelemetryIsActive { get; set; }
    }

    public interface ISettingsService
    {
        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}