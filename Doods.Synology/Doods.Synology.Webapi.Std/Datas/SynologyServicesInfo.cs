using System.Collections.Generic;
using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;
using RestSharp;
using SQLite;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyEntriesInfo
    {
        [JsonProperty("has_fail")]
        public bool HasFail { get; set; }

        [JsonProperty("result")]
        public List<SynoEntry> Result { get; set; }
    }

    public class SynoEntry
    {
        [JsonProperty("api")]
        public string Api { get; set; }

        [JsonProperty("data")]
        public ResultData Data { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }

    public class ResultData
    {
        [JsonProperty("disable_shadow_copy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableShadowCopy { get; set; }

        [JsonProperty("disable_strict_allocate", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableStrictAllocate { get; set; }

        [JsonProperty("enable_dirsort", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableDirsort { get; set; }

        [JsonProperty("enable_durable_handles", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableDurableHandles { get; set; }

        [JsonProperty("enable_enhance_log", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableEnhanceLog { get; set; }

        [JsonProperty("enable_local_master_browser", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableLocalMasterBrowser { get; set; }

        [JsonProperty("enable_mask", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableMask { get; set; }

        [JsonProperty("enable_msdfs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableMsdfs { get; set; }

        [JsonProperty("enable_op_lock", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableOpLock { get; set; }

        [JsonProperty("enable_reset_on_zero_vc", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableResetOnZeroVc { get; set; }

        [JsonProperty("enable_samba", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSamba { get; set; }

        [JsonProperty("enable_smb2_leases", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSmb2Leases { get; set; }

        [JsonProperty("enable_symlink", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSymlink { get; set; }

        [JsonProperty("enable_syno_catia", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSynoCatia { get; set; }

        [JsonProperty("enable_vetofile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableVetofile { get; set; }

        [JsonProperty("enable_widelink", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableWidelink { get; set; }

        [JsonProperty("offline_files_support", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OfflineFilesSupport { get; set; }

        [JsonProperty("smb_encrypt_transport", NullValueHandling = NullValueHandling.Ignore)]
        public long? SmbEncryptTransport { get; set; }

        [JsonProperty("smb_max_protocol", NullValueHandling = NullValueHandling.Ignore)]
        public long? SmbMaxProtocol { get; set; }

        [JsonProperty("smb_min_protocol", NullValueHandling = NullValueHandling.Ignore)]
        public long? SmbMinProtocol { get; set; }

        [JsonProperty("syno_wildcard_search", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SynoWildcardSearch { get; set; }

        [JsonProperty("vetofile", NullValueHandling = NullValueHandling.Ignore)]
        public string Vetofile { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public string Wins { get; set; }

        [JsonProperty("workgroup", NullValueHandling = NullValueHandling.Ignore)]
        public string Workgroup { get; set; }

        [JsonProperty("enable_afp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAfp { get; set; }

        [JsonProperty("enable_disconnect_quick", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableDisconnectQuick { get; set; }

        [JsonProperty("enable_umask", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableUmask { get; set; }

        [JsonProperty("time_machine", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeMachine { get; set; }

        [JsonProperty("enable_nfs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableNfs { get; set; }

        [JsonProperty("enable_nfs_v4", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableNfsV4 { get; set; }

        [JsonProperty("nfs_v4_domain", NullValueHandling = NullValueHandling.Ignore)]
        public string NfsV4Domain { get; set; }

        [JsonProperty("read_size", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReadSize { get; set; }

        [JsonProperty("support_minor_ver", NullValueHandling = NullValueHandling.Ignore)]
        public long? SupportMinorVer { get; set; }

        [JsonProperty("unix_pri_enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UnixPriEnable { get; set; }

        [JsonProperty("write_size", NullValueHandling = NullValueHandling.Ignore)]
        public long? WriteSize { get; set; }

        [JsonProperty("custom_port", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomPort { get; set; }

        [JsonProperty("custom_port_range", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CustomPortRange { get; set; }

        [JsonProperty("enable_ascii", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAscii { get; set; }

        [JsonProperty("enable_fips", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableFips { get; set; }

        [JsonProperty("enable_flow_ctrl", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableFlowCtrl { get; set; }

        [JsonProperty("enable_ftp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableFtp { get; set; }

        [JsonProperty("enable_ftps", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableFtps { get; set; }

        [JsonProperty("enable_fxp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableFxp { get; set; }

        [JsonProperty("ext_ip", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtIp { get; set; }

        [JsonProperty("max_conn_per_ip", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxConnPerIp { get; set; }

        [JsonProperty("maxdownloadrate", NullValueHandling = NullValueHandling.Ignore)]
        public long? Maxdownloadrate { get; set; }

        [JsonProperty("maxuploadrate", NullValueHandling = NullValueHandling.Ignore)]
        public long? Maxuploadrate { get; set; }

        [JsonProperty("portnum", NullValueHandling = NullValueHandling.Ignore)]
        public long? Portnum { get; set; }

        [JsonProperty("timeout", NullValueHandling = NullValueHandling.Ignore)]
        public long? Timeout { get; set; }

        [JsonProperty("use_ext_ip", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UseExtIp { get; set; }

        [JsonProperty("utf8_mode", NullValueHandling = NullValueHandling.Ignore)]
        public long? Utf8Mode { get; set; }

        [JsonProperty("enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }

        [JsonProperty("policy", NullValueHandling = NullValueHandling.Ignore)]
        public string Policy { get; set; }

        [JsonProperty("protocol", NullValueHandling = NullValueHandling.Ignore)]
        public string Protocol { get; set; }

        [JsonProperty("schedule_plan", NullValueHandling = NullValueHandling.Ignore)]
        public string SchedulePlan { get; set; }

        [JsonProperty("enable_log", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableLog { get; set; }

        [JsonProperty("endip", NullValueHandling = NullValueHandling.Ignore)]
        public string Endip { get; set; }

        [JsonProperty("permission", NullValueHandling = NullValueHandling.Ignore)]
        public string Permission { get; set; }

        [JsonProperty("root_path", NullValueHandling = NullValueHandling.Ignore)]
        public string RootPath { get; set; }

        [JsonProperty("startip", NullValueHandling = NullValueHandling.Ignore)]
        public string Startip { get; set; }

        [JsonProperty("enable_custom_config", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableCustomConfig { get; set; }

        [JsonProperty("enable_rsync_account", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableRsyncAccount { get; set; }

        [JsonProperty("rsync_sshd_port", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RsyncSshdPort { get; set; }

        [JsonProperty("enable_domain", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableDomain { get; set; }

        [JsonProperty("shares", NullValueHandling = NullValueHandling.Ignore)]
        public List<ShareService> Shares { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("enable_afp_time_machine", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAfpTimeMachine { get; set; }

        [JsonProperty("enable_smb_time_machine", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSmbTimeMachine { get; set; }

        [JsonProperty("time_machine_disable_shares", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> TimeMachineDisableShares { get; set; }

        [JsonProperty("time_machine_shares", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> TimeMachineShares { get; set; }

        [JsonProperty("afp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Afp { get; set; }

        [JsonProperty("cifs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Cifs { get; set; }

        [JsonProperty("filestation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Filestation { get; set; }

        [JsonProperty("ftp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ftp { get; set; }

        [JsonProperty("tftp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Tftp { get; set; }

        [JsonProperty("webdav", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Webdav { get; set; }

        [JsonProperty("arp_ignore", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ArpIgnore { get; set; }

        [JsonProperty("dns_manual", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DnsManual { get; set; }

        [JsonProperty("dns_primary", NullValueHandling = NullValueHandling.Ignore)]
        public string DnsPrimary { get; set; }

        [JsonProperty("dns_secondary", NullValueHandling = NullValueHandling.Ignore)]
        public string DnsSecondary { get; set; }

        [JsonProperty("enable_windomain", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableWindomain { get; set; }

        [JsonProperty("gateway", NullValueHandling = NullValueHandling.Ignore)]
        public string Gateway { get; set; }

        [JsonProperty("gateway_info", NullValueHandling = NullValueHandling.Ignore)]
        public GatewayInfo GatewayInfo { get; set; }

        [JsonProperty("multi_gateway", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MultiGateway { get; set; }

        [JsonProperty("server_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerName { get; set; }

        [JsonProperty("v6gateway", NullValueHandling = NullValueHandling.Ignore)]
        public string V6Gateway { get; set; }

        [JsonProperty("reflink_copy_enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReflinkCopyEnable { get; set; }

        [JsonProperty("enable_avahi", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableAvahi { get; set; }

        [JsonProperty("enable_custom_domain", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableCustomDomain { get; set; }

        [JsonProperty("enable_hsts", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableHsts { get; set; }

        [JsonProperty("enable_https_redirect", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableHttpsRedirect { get; set; }

        [JsonProperty("enable_max_connections", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableMaxConnections { get; set; }

        [JsonProperty("enable_server_header", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableServerHeader { get; set; }

        [JsonProperty("enable_spdy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSpdy { get; set; }

        [JsonProperty("enable_ssdp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableSsdp { get; set; }

        [JsonProperty("fqdn")]
        public object Fqdn { get; set; }

        [JsonProperty("http_port", NullValueHandling = NullValueHandling.Ignore)]
        public long? HttpPort { get; set; }

        [JsonProperty("https_port", NullValueHandling = NullValueHandling.Ignore)]
        public long? HttpsPort { get; set; }

        [JsonProperty("max_connections", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxConnections { get; set; }

        [JsonProperty("max_connections_limit", NullValueHandling = NullValueHandling.Ignore)]
        public MaxConnectionsLimit MaxConnectionsLimit { get; set; }

        [JsonProperty("server_header", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerHeader { get; set; }

        [JsonProperty("enable_bonjour_support", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableBonjourSupport { get; set; }

        [JsonProperty("enable_wstransfer", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableWstransfer { get; set; }
    }

    public class GatewayInfo
    {
        [JsonProperty("ifname")]
        public string Ifname { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("mask")]
        public string Mask { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("use_dhcp")]
        public bool UseDhcp { get; set; }
    }

    public class MaxConnectionsLimit
    {
        [JsonProperty("lower")]
        public long Lower { get; set; }

        [JsonProperty("upper")]
        public long Upper { get; set; }
    }
}