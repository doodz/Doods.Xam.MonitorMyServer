// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="IOmvService.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/11/27 at 11:07: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IOmvService : INasService, IRpcService
    {
        Task<IEnumerable<PluginInfo>> GetPlugins();

        Task<bool> Connect(string username, string password);
    }
}