using System.Xml.Serialization;

namespace Doods.Framework.Mobile.Std
{
    [XmlRoot("add")]
    public class Setting
    {
        [XmlAttribute("key")] public string Key { get; set; }

        [XmlAttribute("value")] public string Value { get; set; }
    }
}