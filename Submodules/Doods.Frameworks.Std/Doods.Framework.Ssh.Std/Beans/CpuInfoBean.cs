// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="CpuInfoBean.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/01/12 at 17:29: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

namespace Doods.Framework.Ssh.Std.Beans
{
    public class CpuInfoBean
    {
        public string Architecture { get; internal set; }
        public string ByteOrder { get; internal set; }
        public int Cpu { get; internal set; }
        public string OnlineCpuList { get; internal set; }
        public int ThreadPerCore { get; internal set; }
        public int CorePerSocket { get; internal set; }
        public int Socket { get; internal set; }
        public string ModelName { get; internal set; }
        public double CpuMaxMHz { get; internal set; }
        public double CpuMinMHz { get; internal set; }

        public string CpuOpMode { get; internal set; }
        public string VendorId { get; internal set; }
    }
}