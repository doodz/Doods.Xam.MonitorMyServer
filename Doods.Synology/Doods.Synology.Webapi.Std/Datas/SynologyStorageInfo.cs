// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SynologyStorageInfo.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/09/17 at 16:10: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyStorageInfo
    {
        [JsonProperty("disks")]
        public List<Disk> Disks { get; set; }

        [JsonProperty("env")]
        public Env Env { get; set; }

        [JsonProperty("hotSpareConf")]
        public HotSpareConf HotSpareConf { get; set; }

        [JsonProperty("hotSpares")]
        public List<object> HotSpares { get; set; }

        [JsonProperty("iscsiLuns")]
        public List<object> IscsiLuns { get; set; }

        [JsonProperty("iscsiTargets")]
        public List<object> IscsiTargets { get; set; }

        [JsonProperty("ports")]
        public List<object> Ports { get; set; }

        [JsonProperty("ssdCaches")]
        public List<object> SsdCaches { get; set; }

        [JsonProperty("storagePools")]
        public List<StoragePool> StoragePools { get; set; }

        [JsonProperty("volumes")]
        public List<Volume> Volumes { get; set; }
    }
}