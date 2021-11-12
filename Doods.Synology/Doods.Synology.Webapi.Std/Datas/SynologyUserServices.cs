using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SynologyUserServices
    {
        [JsonProperty("ActionPrivilege")]
        public List<object> ActionPrivilege { get; set; }

        [JsonProperty("AppPrivilege")]
        public Dictionary<string, bool> AppPrivilege { get; set; }

        [JsonProperty("GroupSettings")]
        public object GroupSettings { get; set; }

        [JsonProperty("ServiceStatus")]
        public ServiceStatus ServiceStatus { get; set; }

        [JsonProperty("Session")]
        public Session Session { get; set; }

        [JsonProperty("UrlTag")]
        public UrlTag UrlTag { get; set; }

        [JsonProperty("UserSettings")]
        public UserSettings UserSettings { get; set; }
    }
    internal class PurpleParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Newtonsoft.Json.JsonSerializationException("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
    }

    internal class FluffyParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }

            throw new Newtonsoft.Json.JsonSerializationException("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (long) untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
    }

    public partial class ServiceStatus
    {
        [JsonProperty("SYNO.SDS.PersonalPhotoStation")]
        public bool SynoSdsPersonalPhotoStation { get; set; }
    }

    public partial class Session
    {
        [JsonProperty("AdvControlPanel")]
        public bool AdvControlPanel { get; set; }

        [JsonProperty("authType")]
        public string AuthType { get; set; }

        [JsonProperty("boot_done")]
        public bool BootDone { get; set; }

        [JsonProperty("builddate")]
        public string Builddate { get; set; }

        [JsonProperty("buildphase")]
        public string Buildphase { get; set; }

        [JsonProperty("date_format")]
        public string DateFormat { get; set; }

        [JsonProperty("domainUser")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public bool DomainUser { get; set; }

        [JsonProperty("dsm_timeout")]
        public long DsmTimeout { get; set; }

        [JsonProperty("fullversion")]
        public string Fullversion { get; set; }

        [JsonProperty("gpo_enable_java")]
        public string GpoEnableJava { get; set; }

        [JsonProperty("ha_allow_bond_manage")]
        public bool HaAllowBondManage { get; set; }

        [JsonProperty("ha_handle_set_ovs")]
        public bool HaHandleSetOvs { get; set; }

        [JsonProperty("ha_heartbeat_ip_list")]
        public List<string> HaHeartbeatIpList { get; set; }

        [JsonProperty("ha_hide_hw_setting")]
        public bool HaHideHwSetting { get; set; }

        [JsonProperty("ha_hide_ntb")]
        public bool HaHideNtb { get; set; }

        [JsonProperty("ha_hide_ntp_setting")]
        public bool HaHideNtpSetting { get; set; }

        [JsonProperty("ha_hw_spectre_meltdown")]
        public bool HaHwSpectreMeltdown { get; set; }

        [JsonProperty("ha_not_support_bridge")]
        public bool HaNotSupportBridge { get; set; }

        [JsonProperty("ha_not_support_ipv6")]
        public bool HaNotSupportIpv6 { get; set; }

        [JsonProperty("ha_not_support_pppoe")]
        public bool HaNotSupportPppoe { get; set; }

        [JsonProperty("ha_not_support_usb_modem")]
        public bool HaNotSupportUsbModem { get; set; }

        [JsonProperty("ha_running")]
        public bool HaRunning { get; set; }

        [JsonProperty("ha_safemode")]
        public bool HaSafemode { get; set; }

        [JsonProperty("ha_support_controller_notify")]
        public bool HaSupportControllerNotify { get; set; }

        [JsonProperty("ha_support_node_notify")]
        public bool HaSupportNodeNotify { get; set; }

        [JsonProperty("ha_support_pw_btn")]
        public bool HaSupportPwBtn { get; set; }

        [JsonProperty("has_ha_if")]
        public bool HasHaIf { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("ip_country")]
        public string IpCountry { get; set; }

        [JsonProperty("isLogined")]
        public bool IsLogined { get; set; }

        [JsonProperty("isMobile")]
        public bool IsMobile { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("is_dual_chain")]
        public bool IsDualChain { get; set; }

        [JsonProperty("is_ha_empty_passive")]
        public bool IsHaEmptyPassive { get; set; }

        [JsonProperty("is_ha_upgrading")]
        public bool IsHaUpgrading { get; set; }

        [JsonProperty("is_hybrid_ha")]
        public bool IsHybridHa { get; set; }

        [JsonProperty("is_secure")]
        public bool IsSecure { get; set; }

        [JsonProperty("is_upgrading")]
        public bool IsUpgrading { get; set; }

        [JsonProperty("join_dsm_cms")]
        public bool JoinDsmCms { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("majorversion")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long Majorversion { get; set; }

        [JsonProperty("manage_eth_in_ha_pkg")]
        public bool ManageEthInHaPkg { get; set; }

        [JsonProperty("manage_hostname_in_ha_pkg")]
        public bool ManageHostnameInHaPkg { get; set; }

        [JsonProperty("manage_pw_btn_in_ha_pkg")]
        public bool ManagePwBtnInHaPkg { get; set; }

        [JsonProperty("minorversion")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long Minorversion { get; set; }

        [JsonProperty("productversion")]
        public string Productversion { get; set; }

        [JsonProperty("remove_banner")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public bool RemoveBanner { get; set; }

        [JsonProperty("sso_appid")]
        public string SsoAppid { get; set; }

        [JsonProperty("sso_server")]
        public string SsoServer { get; set; }

        [JsonProperty("sso_support")]
        public bool SsoSupport { get; set; }

        [JsonProperty("sys_lang")]
        public string SysLang { get; set; }

        [JsonProperty("theme_cls")]
        public string ThemeCls { get; set; }

        [JsonProperty("time_format")]
        public string TimeFormat { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("version")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public long Version { get; set; }
    }

    public partial class UrlTag
    {
    }

    public partial class UserSettings
    {
        [JsonProperty("BackgroundTask")]
        public UrlTag BackgroundTask { get; set; }

        [JsonProperty("Desktop")]
        public Desktop Desktop { get; set; }

        [JsonProperty("SYNO.SDS.App.FileStation3.Instance")]
        public SynoSdsAppFileStation3Instance SynoSdsAppFileStation3Instance { get; set; }

        [JsonProperty("SYNO.SDS.App.PersonalSettings.Instance")]
        public SynoSdsAppPersonalSettingsInstance SynoSdsAppPersonalSettingsInstance { get; set; }

        [JsonProperty("SYNO.SDS.App.PromotionApp")]
        public SynoSdsAppPromotionApp SynoSdsAppPromotionApp { get; set; }

        [JsonProperty("SYNO.SDS.Drive.Application")]
        public SynoSdsDriveApplication SynoSdsDriveApplication { get; set; }

        [JsonProperty("SYNO.SDS.SynologyApplicationService")]
        public SynoSdsSynologyApplicationService SynoSdsSynologyApplicationService { get; set; }
    }

    public partial class Desktop
    {
        [JsonProperty("ShortcutItems")]
        public List<ShortcutItem> ShortcutItems { get; set; }

        [JsonProperty("new_app_list")]
        public List<object> NewAppList { get; set; }

        [JsonProperty("valid_appview_order")]
        public List<string> ValidAppviewOrder { get; set; }
    }

    public partial class ShortcutItem
    {
        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public partial class SynoSdsAppFileStation3Instance
    {
        [JsonProperty("restoreSizePos")]
        public RestoreSizePos RestoreSizePos { get; set; }

        [JsonProperty("treePanelWidth")]
        public long TreePanelWidth { get; set; }
    }

    public partial class RestoreSizePos
    {
        [JsonProperty("fromRestore")]
        public bool FromRestore { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("maximized")]
        public bool Maximized { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial class SynoSdsAppPersonalSettingsInstance
    {
        [JsonProperty("search_count")]
        public long SearchCount { get; set; }
    }

    public partial class SynoSdsAppPromotionApp
    {
        [JsonProperty("show_quick_tour_tray")]
        public bool ShowQuickTourTray { get; set; }

        [JsonProperty("show_upgrade_welcome")]
        public bool ShowUpgradeWelcome { get; set; }
    }

    public partial class SynoSdsDriveApplication
    {
        [JsonProperty("gridsortstates")]
        public Gridsortstates Gridsortstates { get; set; }

        [JsonProperty("viewmode")]
        public string Viewmode { get; set; }
    }

    public partial class Gridsortstates
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }
    }

    public partial class SynoSdsSynologyApplicationService
    {
        [JsonProperty("app_order")]
        public List<string> AppOrder { get; set; }
    }

}