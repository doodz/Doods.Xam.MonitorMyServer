// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SynolgySharesInfo.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/11/27 at 09:30: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynolgySharesInfo
    {
        [JsonProperty("shares")]
        public List<Share> Shares { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}