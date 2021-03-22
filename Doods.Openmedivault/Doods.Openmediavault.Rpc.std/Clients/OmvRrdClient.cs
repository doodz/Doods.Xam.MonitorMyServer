using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvRrdClient : OmvServiceClient, IRrdService
    {
        private readonly IRpcClient _getclient;

        public OmvRrdClient(IRpcClient client) : base(client)
        {
            ServiceName = "Rrd";
            _getclient = client;
        }

        public Task<IEnumerable<string>> ListRdd()
        {
            return _getclient.ListRrdFilesAsync();
        }


        public Task<IEnumerable<byte[]>> GetRrdFiles(IEnumerable<string> fileNames)
        {
            if (RequestType == RequestType.http)
            {
                var toto = "rrd.php?name=";
                return _getclient.GetRrdFilesAsync(fileNames);
            }

            if (RequestType == RequestType.ssh)

            {
                var tutu = "/var/lib/openmediavault/rrd/";
                return _getclient.GetRrdFilesAsync(fileNames.Select(f => tutu + f));
            }

            return Task.FromResult(default(IEnumerable<byte[]>));
        }

        public Task<byte[]> GetRrdFile(string fileName)
        {
            if (RequestType == RequestType.http)
            {
                var toto = "rrd.php?name=";
                return _getclient.GetRrdFileAsync(fileName);
            }

            if (RequestType == RequestType.ssh)

            {
                var tutu = "/var/lib/openmediavault/rrd/";
                return _getclient.GetRrdFileAsync(tutu + fileName);
            }

            return Task.FromResult(default(byte[]));
        }

        public Task<string> GenerateRdd()
        {
            var request = NewRequest("generate");

            var result = RunCmd<string>(request);

            return result;
        }
    }
}