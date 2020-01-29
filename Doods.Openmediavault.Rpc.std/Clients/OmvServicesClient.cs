﻿using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvServicesClient : OmvServiceClient
    {
       
        public OmvServicesClient(IRpcClient client) : base(client)
        {
            ServiceName = "Services";
        }

        public Task<ResponseArray<ServicesStatus>> GetServicesStatus()
        {
            var request = NewRequest("getStatus");
            request.Params = new ParamsListFilter();
           
            var result = RunCmd<ResponseArray<ServicesStatus>>(request);

            return result;
        }
    }
}