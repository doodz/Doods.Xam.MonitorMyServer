﻿using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvExecClient : OmvServiceClient
    {
        public OmvExecClient(IRpcClient client) : base(client)
        {
            ServiceName = "Exec";
        }


        public Task<IsRunning> IsRunning(string filename)
        {
            var request = NewRequest("isRunning");
            request.Params = new {filename};
            var result = RunCmd<IsRunning>(request);
            return result;
        }

        public Task<Output<T>> GetOutput<T>(string filename, int pos)
        {
            var request = NewRequest("getOutput");
            request.Params = new {filename, pos};
            var result = RunCmd<Output<T>>(request);
            return result;
        }
    }
}