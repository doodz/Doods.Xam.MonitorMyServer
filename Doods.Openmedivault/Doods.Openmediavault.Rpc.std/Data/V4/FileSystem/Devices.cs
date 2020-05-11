using System;
using System.Collections.Generic;
using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4.FileSystem
{
    public class Devices : IOmvObject
    {
        [JsonProperty("uuid")] public Guid Uuid { get; set; }

        [JsonProperty("comment")] public string Comment { get; set; }

        [JsonProperty("_used")] public bool Used { get; set; }

        [JsonProperty("_readonly")] public bool Readonly { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("devicename")] public string Devicename { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("netmask")] public string Netmask { get; set; }

        [JsonProperty("gateway")] public string Gateway { get; set; }

        [JsonProperty("method6", NullValueHandling = NullValueHandling.Ignore)]
        public string Method6 { get; set; }

        [JsonProperty("address6")] public string Address6 { get; set; }

        [JsonProperty("netmask6")] public long Netmask6 { get; set; }

        [JsonProperty("gateway6")] public string Gateway6 { get; set; }

        [JsonProperty("dnsnameservers", NullValueHandling = NullValueHandling.Ignore)]
        public string Dnsnameservers { get; set; }

        [JsonProperty("dnssearch", NullValueHandling = NullValueHandling.Ignore)]
        public string Dnssearch { get; set; }

        [JsonProperty("mtu")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Mtu { get; set; }

        [JsonProperty("wol", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Wol { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public string Options { get; set; }

        [JsonProperty("slaves", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Slaves { get; set; }

        [JsonProperty("bondprimary", NullValueHandling = NullValueHandling.Ignore)]
        public string Bondprimary { get; set; }

        [JsonProperty("bondmode", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bondmode { get; set; }

        [JsonProperty("bondmiimon", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bondmiimon { get; set; }

        [JsonProperty("bonddowndelay", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bonddowndelay { get; set; }

        [JsonProperty("bondupdelay", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bondupdelay { get; set; }

        [JsonProperty("vlanid")] public long Vlanid { get; set; }

        [JsonProperty("vlanrawdevice", NullValueHandling = NullValueHandling.Ignore)]
        public string Vlanrawdevice { get; set; }

        [JsonProperty("wpassid", NullValueHandling = NullValueHandling.Ignore)]
        public string Wpassid { get; set; }

        [JsonProperty("wpapsk", NullValueHandling = NullValueHandling.Ignore)]
        public string Wpapsk { get; set; }

        [JsonProperty("ether")] public string Ether { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("link")] public bool Link { get; set; }

        [JsonProperty("vlan")] public bool Vlan { get; set; }

        [JsonProperty("speed")] public long Speed { get; set; }

        [JsonProperty("prefix")] public Prefix Prefix { get; set; }

        [JsonProperty("prefix6")] public bool Prefix6 { get; set; }
    }

    public struct Prefix
    {
        public bool? Bool;
        public long? Integer;

        public static implicit operator Prefix(bool Bool)
        {
            return new Prefix {Bool = Bool};
        }

        public static implicit operator Prefix(long Integer)
        {
            return new Prefix {Integer = Integer};
        }
    }
}