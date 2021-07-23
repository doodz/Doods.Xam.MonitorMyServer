using System;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Servicies
{
    public class SettingsServiceBase : ISettingsBase, ISettingsService
    {
        public bool TelemetryIsActive { get; set; } = true;

        public Task AddOrUpdateValue(string key, bool value)
        {
            return AddOrUpdateValueInternal(key, value);
        }

        public Task AddOrUpdateValue(string key, string value)
        {
            return AddOrUpdateValueInternal(key, value);
        }

        public bool GetValueOrDefault(string key, bool defaultValue)
        {
            return GetValueOrDefaultInternal(key, defaultValue);
        }

        public string GetValueOrDefault(string key, string defaultValue)
        {
            return GetValueOrDefaultInternal(key, defaultValue);
        }

        private async Task AddOrUpdateValueInternal<T>(string key, T value)
        {
            if (value == null) await Remove(key);

            Application.Current.Properties[key] = value;
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save: " + key, " Message: " + ex.Message);
            }
        }

        private T GetValueOrDefaultInternal<T>(string key, T defaultValue = default)
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key)) value = Application.Current.Properties[key];
            return null != value ? (T) value : defaultValue;
        }

        private async Task Remove(string key)
        {
            try
            {
                if (Application.Current.Properties[key] != null)
                {
                    Application.Current.Properties.Remove(key);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to remove: " + key, " Message: " + ex.Message);
            }
        }
    }
}