using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Disk
    {
        [JsonProperty("adv_progress")]
        public string AdvProgress { get; set; }

        [JsonProperty("adv_status")]
        public string AdvStatus { get; set; }

        [JsonProperty("below_remain_life_mail_notify_thr")]
        public bool BelowRemainLifeMailNotifyThr { get; set; }

        [JsonProperty("below_remain_life_show_thr")]
        public bool BelowRemainLifeShowThr { get; set; }

        [JsonProperty("below_remain_life_thr")]
        public bool BelowRemainLifeThr { get; set; }

        [JsonProperty("compatibility")]
        public string Compatibility { get; set; }

        [JsonProperty("container")]
        public Container Container { get; set; }

        [JsonProperty("container_id")]
        public long ContainerId { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("disable_secera")]
        public bool DisableSecera { get; set; }

        [JsonProperty("diskType")]
        public string DiskType { get; set; }

        [JsonProperty("disk_code")]
        public string DiskCode { get; set; }

        [JsonProperty("erase_time")]
        public long EraseTime { get; set; }

        [JsonProperty("exceed_bad_sector_thr")]
        public bool ExceedBadSectorThr { get; set; }

        [JsonProperty("firm")]
        public string Firm { get; set; }

        [JsonProperty("has_system")]
        public bool HasSystem { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ihm_testing")]
        public bool IhmTesting { get; set; }

        [JsonProperty("is4Kn")]
        public bool Is4Kn { get; set; }

        [JsonProperty("isSsd")]
        public bool IsSsd { get; set; }

        [JsonProperty("isSynoDrive")]
        public bool IsSynoDrive { get; set; }

        [JsonProperty("isSynoPartition")]
        public bool IsSynoPartition { get; set; }

        [JsonProperty("is_bundle_ssd")]
        public bool IsBundleSsd { get; set; }

        [JsonProperty("is_erasing")]
        public bool IsErasing { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("num_id")]
        public long NumId { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("overview_status")]
        public string OverviewStatus { get; set; }

        [JsonProperty("pciSlot")]
        public long PciSlot { get; set; }

        [JsonProperty("perf_testing")]
        public bool PerfTesting { get; set; }

        [JsonProperty("portType")]
        public string PortType { get; set; }

        [JsonProperty("remain_life")]
        public long RemainLife { get; set; }

        [JsonProperty("remain_life_danger")]
        public bool RemainLifeDanger { get; set; }

        [JsonProperty("remote_info")]
        public RemoteInfo RemoteInfo { get; set; }

        [JsonProperty("sb_days_left")]
        public long SbDaysLeft { get; set; }

        [JsonProperty("sb_days_left_critical")]
        public bool SbDaysLeftCritical { get; set; }

        [JsonProperty("sb_days_left_warning")]
        public bool SbDaysLeftWarning { get; set; }

        [JsonProperty("serial")]
        public string Serial { get; set; }

        [JsonProperty("size_total")]
        public string SizeTotal { get; set; }

        [JsonProperty("slot_id")]
        public long SlotId { get; set; }

        [JsonProperty("smart_progress")]
        public string SmartProgress { get; set; }

        [JsonProperty("smart_status")]
        public string SmartStatus { get; set; }

        [JsonProperty("smart_test_limit")]
        public long SmartTestLimit { get; set; }

        [JsonProperty("smart_testing")]
        public bool SmartTesting { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("support")]
        public bool Support { get; set; }

        [JsonProperty("temp")]
        public long Temp { get; set; }

        [JsonProperty("testing_progress")]
        public string TestingProgress { get; set; }

        [JsonProperty("testing_type")]
        public string TestingType { get; set; }

        [JsonProperty("tray_status")]
        public string TrayStatus { get; set; }

        [JsonProperty("unc")]
        public long Unc { get; set; }

        [JsonProperty("used_by")]
        public string UsedBy { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }
    }
}