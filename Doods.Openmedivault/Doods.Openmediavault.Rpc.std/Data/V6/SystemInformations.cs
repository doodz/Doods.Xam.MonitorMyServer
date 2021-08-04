using System.Collections.Generic;
using System.Linq;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Openmediavault.Rpc.Std.Seruializer;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V6
{
    public class OMVInformations : IOmvObject
    {
        [JsonIgnore] public bool LegacyMode = false;
        [JsonProperty("ts")] public long Ts { get; set; }

        [JsonProperty("time")] public string Time { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("version")] public string Version { get; set; }

        [JsonProperty("cpuModelName")] public string CpuModelName { get; set; }

        [JsonProperty("cpuUsage")] public double CpuUsage { get; set; }

        [JsonProperty("memTotal")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MemTotal { get; set; }

        [JsonProperty("memUsed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MemUsed { get; set; }

        [JsonProperty("memFree")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MemFree { get; set; }

        [JsonProperty("kernel")] public string Kernel { get; set; }

        [JsonProperty("uptime")] public string Uptime { get; set; }

        [JsonProperty("loadAverage")]
        public Dictionary<string, long> LoadAverageDico
        {
            get { return _loadAverageDico;}
            set
            {
                _loadAverageDico = value;
                if (value != null)
                {
                    var item = string.Empty;
                    foreach (var keyValuePair in value)
                    {
                        item += keyValuePair.Key + ":" + keyValuePair.Value+";";
                    }

                    LoadAverage = item;
                }
                else
                    LoadAverage = null;
            } }

        private Dictionary<string, long> _loadAverageDico { get; set; }
        public string LoadAverage { get; set; }

        [JsonProperty("configDirty")] public bool ConfigDirty { get; set; }

        [JsonProperty("rebootRequired")] public bool RebootRequired { get; set; }

        [JsonProperty("pkgUpdatesAvailable")] public bool PkgUpdatesAvailable { get; set; }


        public V5.OMVInformations ToV5()
        {
            var obj = new V5.OMVInformations();
            obj.ConfigDirty = ConfigDirty;
            obj.CpuModelName = CpuModelName;
            obj.CpuUsage = CpuUsage;
            obj.Hostname = Hostname;
            obj.Kernel = Kernel;
            obj.LegacyMode = LegacyMode;
            obj.LoadAverage = LoadAverage;
            obj.MemTotal = MemTotal;
            obj.MemUsed = MemUsed;
            obj.PkgUpdatesAvailable = PkgUpdatesAvailable;
            obj.RebootRequired = RebootRequired;
            obj.ConfigDirty = ConfigDirty;
            obj.Version = Version;
            obj.Uptime = Uptime;
            obj.Ts = Ts;
            obj.Time = Time;

            return obj;
        }
    }
}