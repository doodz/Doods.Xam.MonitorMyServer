using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public  class HddInfo
    {
        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("diskPath")]
        public string DiskPath { get; set; }

        [JsonProperty("diskType")]
        public string DiskType { get; set; }

        [JsonProperty("diskno")]
        public string Diskno { get; set; }

        [JsonProperty("ebox_order")]
        public long EboxOrder { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("overview_status")]
        public string OverviewStatus { get; set; }

        [JsonProperty("pciSlot")]
        public long PciSlot { get; set; }

        [JsonProperty("portType")]
        public string PortType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("temp")]
        public long Temp { get; set; }

        [JsonProperty("testing_progress")]
        public string TestingProgress { get; set; }

        [JsonProperty("testing_type")]
        public string TestingType { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }
    }
}