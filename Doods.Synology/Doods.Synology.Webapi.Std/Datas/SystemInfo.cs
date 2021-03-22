using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SystemInfo
    {
        [JsonProperty("cpu_clock_speed")] public long CpuClockSpeed { get; set; }

        [JsonProperty("cpu_cores")] public long CpuCores { get; set; }

        [JsonProperty("cpu_family")] public string CpuFamily { get; set; }

        [JsonProperty("cpu_series")] public string CpuSeries { get; set; }

        [JsonProperty("cpu_vendor")] public string CpuVendor { get; set; }

        [JsonProperty("enabled_ntp")] public bool EnabledNtp { get; set; }

        [JsonProperty("firmware_date")] public string FirmwareDate { get; set; }

        [JsonProperty("firmware_ver")] public string FirmwareVer { get; set; }

        [JsonProperty("model")] public string Model { get; set; }

        [JsonProperty("ntp_server")] public string NtpServer { get; set; }

        [JsonProperty("ram_size")] public long RamSize { get; set; }

        [JsonProperty("sata_dev")] public List<object> SataDev { get; set; }

        [JsonProperty("serial")] public string Serial { get; set; }

        [JsonProperty("support_esata")] public string SupportEsata { get; set; }

        [JsonProperty("sys_temp")] public long SysTemp { get; set; }

        [JsonProperty("sys_tempwarn")] public bool SysTempwarn { get; set; }

        [JsonProperty("systempwarn")] public bool Systempwarn { get; set; }

        [JsonProperty("temperature_warning")] public bool TemperatureWarning { get; set; }

        [JsonProperty("time")] public DateTimeOffset Time { get; set; }

        [JsonProperty("time_zone")] public string TimeZone { get; set; }

        [JsonProperty("time_zone_desc")] public string TimeZoneDesc { get; set; }

        [JsonProperty("up_time")] public string UpTime { get; set; }

        [JsonProperty("usb_dev")] public List<object> UsbDev { get; set; }
    }
}