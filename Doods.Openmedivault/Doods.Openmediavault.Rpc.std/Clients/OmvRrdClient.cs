using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvRrdClient : OmvServiceClient, IRrdService
    {
        private readonly string _extension = "png";
        private readonly string[] _typeDate = new string[5] { "hour", "day", "week", "month", "year" };
        private readonly string[] _typeInfo = new string[4] { "df-root", "memory", "load", "cpu" };

        //df-srv-dev-disk-by-label-OpenMediaVault-hour
        //df-srv-dev-disk-by-id-wwn-0x6001405d8…8fcd44afd863fdf-part1-hour

        /*
         * rrd.php?name=memory-day.png&time=1584971861096
         * rrd.php?name=memory-week.png&time=1584971861096
         * rrd.php?name=memory-month.png&time=1584971861096
         * rrd.php?name=memory-year.png&time=1584971861096
         * rrd.php?name=df-root-hour.png&time=1584971861972
         * rrd.php?name=df-root-day.png&time=1584971861972
         * rrd.php?name=df-root-week.png&time=1584971861972
         * rrd.php?name=df-root-month.png&time=1584971861972
         * rrd.php?name=df-root-year.png&time=1584971861972
         * rrd.php?name=df-srv-dev-disk-by-label-OpenMediaVault-hour.png&time=1584971861976
         * rrd.php?name=df-srv-dev-disk-by-label-LEDISCKC-hour.png&time=1584971861974
         * rrd.php?name=df-srv-dev-disk-by-id-wwn-0x6001405d8…8fcd44afd863fdf-part1-hour.png&time=1584971861973
         * rrd.php?name=load-day.png&time=1584971861095
         * rrd.php?name=cpu-0-hour.png&time=1584971861097
         *
        */
        private IRpcClient _getclient;
        public OmvRrdClient(IRpcClient client) : base(client)
        {
            ServiceName = "Rrd";
            _getclient = client;
        }

        public Task<IEnumerable<string>> ListRdd()
        {
            var lst = new List<string>();
            foreach (var i in _typeInfo)
                foreach (var d in _typeDate)
                    lst.Add($"{i}-{d}.{_extension}");

            if (RequestType == RequestType.http) return Task.FromResult(lst.AsEnumerable());


            return Task.FromResult(lst.AsEnumerable());
        }


        public Task<IEnumerable<byte[]>> GetRrdFiles(IEnumerable<string> fileNames)
        {
            if (RequestType == RequestType.http)
            {
                var toto = "rrd.php?name=";
                return _getclient.GetRrdFilesAsync(fileNames);

            }
            else if (RequestType == RequestType.ssh)

            {
                var tutu = "/var/lib/openmediavault/rrd/";
                return _getclient.GetRrdFilesAsync(fileNames.Select(f=> tutu+f));
            }

            return Task.FromResult(default(IEnumerable<byte[]>));
        }

        public Task<byte[]> GetRrdFile(string fileName)
        {
            if (RequestType == RequestType.http)
            {
                var toto="rrd.php?name=";
                return _getclient.GetRrdFileAsync(fileName);

            }
            else if (RequestType == RequestType.ssh)

            {
                var tutu="/var/lib/openmediavault/rrd/";
                return _getclient.GetRrdFileAsync(tutu+fileName);
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