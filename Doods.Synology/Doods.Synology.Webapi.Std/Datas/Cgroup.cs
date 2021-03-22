// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="Cgroup.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/12/04 at 14:17: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Cgroup
    {
        [JsonProperty("cpuFraction")] public long CpuFraction { get; set; }

        [JsonProperty("cpu_time")] public long CpuTime { get; set; }

        [JsonProperty("memory")] public long Memory { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("pids")] public List<long> Pids { get; set; }
    }
}