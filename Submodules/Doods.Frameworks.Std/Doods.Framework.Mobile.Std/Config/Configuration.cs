using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Mobile.Std.Config
{
    public class Configuration : IConfiguration
    {
        private readonly IDeviceInfo _deviceInfo;

        private readonly string[] _excludePropertyName =
        {
            "IdentityClientId", "IdentityClientSecret", "IdentityRedirectUrl"
        };


        private ConfigLoader _config;

        private Dictionary<string, string> _values;

        public Configuration(IDeviceInfo deviceInfo)
        {
            _deviceInfo = deviceInfo;
        }

        public bool ClearLocalData => GetBoolean(nameof(ClearLocalData));


        public string AdsKey => GetString(nameof(AdsKey));
        public string RewardedVideoKey => GetString(nameof(RewardedVideoKey));
        public string MobileCenterKey => GetString(nameof(MobileCenterKey));
        public string AppAdsKey => GetString(nameof(AppAdsKey));
        public bool ModeOmvOnlyKey => GetBoolean(nameof(ModeOmvOnlyKey));
        public string HockeyAppKey => GetString(nameof(HockeyAppKey));

        public string NumeroVersion => $"{_deviceInfo.Version}.{_deviceInfo.Build}";

        public void LoadConfiguration(XmlReader xmlContent)
        {
            _config = new XmlSerializer(typeof(ConfigLoader)).Deserialize(xmlContent) as ConfigLoader;
            _values = _config?.Settings.ToDictionary(c => c.Key, c => c.Value);
        }

        public bool IsExcluded(string propertyName)
        {
            return _excludePropertyName.Contains(propertyName);
        }

        private string GetString(string key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return null;
        }

        private bool GetBoolean(string key)
        {
            if (_values.ContainsKey(key))
                return _values[key] == "1" || _values[key].ToUpper() == "TRUE";

            return false;
        }

        private int GetInteger(string key)
        {
            return _values.ContainsKey(key) ? _values[key].ToInteger() : 0;
        }
    }
}