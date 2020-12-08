// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="OmvShareMgmtClient.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/11/27 at 09:37: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.SharedFolders;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvShareMgmtClient : OmvServiceClient
    {
        public OmvShareMgmtClient(IRpcClient client) : base(client)
        {
            ServiceName = "ShareMgmt";
        }

        public Task<ResponseArray<SharedFolder>> GetSharedFolders()
        {

            var request = NewRequest("getList");
            request.Params = new {  sortfield = "name", start = 0, limit = 50,sortdir = "DESC" };
            var result = RunCmd<ResponseArray<SharedFolder>>(request);
            return result;
        }
    }
}