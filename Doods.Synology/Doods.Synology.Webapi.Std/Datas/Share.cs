using System;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Share
    {
        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("enable_recycle_bin")] public bool EnableRecycleBin { get; set; }

        [JsonProperty("enable_share_compress")]
        public bool EnableShareCompress { get; set; }

        [JsonProperty("enable_share_cow")] public bool EnableShareCow { get; set; }

        [JsonProperty("enc_auto_mount")] public bool EncAutoMount { get; set; }

        [JsonProperty("encryption")] public long Encryption { get; set; }

        [JsonProperty("force_readonly_reason")]
        public string ForceReadonlyReason { get; set; }

        [JsonProperty("hidden")] public bool Hidden { get; set; }

        [JsonProperty("is_aclmode")] public bool IsAclmode { get; set; }

        [JsonProperty("is_block_snap_action")] public bool IsBlockSnapAction { get; set; }

        [JsonProperty("is_cluster_share")] public bool IsClusterShare { get; set; }

        [JsonProperty("is_cold_storage_share")]
        public bool IsColdStorageShare { get; set; }

        [JsonProperty("is_exfat_share")] public bool IsExfatShare { get; set; }

        [JsonProperty("is_force_readonly")] public bool IsForceReadonly { get; set; }

        [JsonProperty("is_share_moving")] public bool IsShareMoving { get; set; }

        [JsonProperty("is_support_acl")] public bool IsSupportAcl { get; set; }

        [JsonProperty("is_sync_share")] public bool IsSyncShare { get; set; }

        [JsonProperty("is_usb_share")] public bool IsUsbShare { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("quota_value")] public long QuotaValue { get; set; }

        [JsonProperty("recycle_bin_admin_only")]
        public bool RecycleBinAdminOnly { get; set; }

        [JsonProperty("share_quota_used")] public double ShareQuotaUsed { get; set; }

        [JsonProperty("support_action")] public long SupportAction { get; set; }

        [JsonProperty("support_snapshot")] public bool SupportSnapshot { get; set; }

        [JsonProperty("task_id")] public string TaskId { get; set; }

        [JsonProperty("unite_permission")] public bool UnitePermission { get; set; }

        [JsonProperty("uuid")] public Guid Uuid { get; set; }

        [JsonProperty("vol_path")] public string VolPath { get; set; }

        [JsonProperty("enc_aes_length", NullValueHandling = NullValueHandling.Ignore)]
        public string EncAesLength { get; set; }
    }
}