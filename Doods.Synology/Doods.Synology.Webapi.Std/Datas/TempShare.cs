using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class TempShare
    {
        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("shares")]
        public List<SimpleShare> Shares { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
    public partial class Owner
    {
        [JsonProperty("gid")]
        public long Gid { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
    public  class Acl
    {
        [JsonProperty("append")]
        public bool Append { get; set; }

        [JsonProperty("del")]
        public bool Del { get; set; }

        [JsonProperty("exec")]
        public bool Exec { get; set; }

        [JsonProperty("read")]
        public bool Read { get; set; }

        [JsonProperty("write")]
        public bool Write { get; set; }
    }
    public partial class AdvRight
    {
        [JsonProperty("disable_download")]
        public bool DisableDownload { get; set; }

        [JsonProperty("disable_list")]
        public bool DisableList { get; set; }

        [JsonProperty("disable_modify")]
        public bool DisableModify { get; set; }
    }

    public class Perm
    {
        [JsonProperty("acl")]
        public Acl Acl { get; set; }

        [JsonProperty("acl_enable")]
        public bool AclEnable { get; set; }

        [JsonProperty("adv_right")]
        public AdvRight AdvRight { get; set; }

        [JsonProperty("is_acl_mode")]
        public bool IsAclMode { get; set; }

        [JsonProperty("is_share_readonly")]
        public bool IsShareReadonly { get; set; }

        [JsonProperty("posix")]
        public long Posix { get; set; }

        [JsonProperty("share_right")]
        public string ShareRight { get; set; }
    }

    public class Additional
    {
        [JsonProperty("indexed")]
        public bool Indexed { get; set; }

        [JsonProperty("mount_point_type")]
        public string MountPointType { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("perm")]
        public Perm Perm { get; set; }

        [JsonProperty("real_path")]
        public string RealPath { get; set; }

        [JsonProperty("sync_share")]
        public bool SyncShare { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("volume_status")]
        public VolumeStatus VolumeStatus { get; set; }
    }
    public partial class VolumeStatus
    {
        [JsonProperty("freespace")]
        public long Freespace { get; set; }

        [JsonProperty("readonly")]
        public bool Readonly { get; set; }

        [JsonProperty("totalspace")]
        public long Totalspace { get; set; }
    }
    public partial class Time
    {
        [JsonProperty("atime")]
        public long Atime { get; set; }

        [JsonProperty("crtime")]
        public long Crtime { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("mtime")]
        public long Mtime { get; set; }
    }
    public class SimpleShare
    {
        [JsonProperty("additional")]
        public Additional Additional { get; set; }

        [JsonProperty("isdir")]
        public bool Isdir { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}