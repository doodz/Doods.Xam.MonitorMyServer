using System.Collections.Generic;
using System.Xml.Serialization;

namespace Doods.Framework.Mobile.Std
{
    [XmlRoot("configuration")]
    public class ConfigLoader
    {
        public ConfigLoader()
        {
            Settings = new List<Setting>();
        }

        [XmlArray("appSettings")]
        [XmlArrayItem(typeof(Setting), ElementName = "add")]
        public List<Setting> Settings { get; set; }
    }
}