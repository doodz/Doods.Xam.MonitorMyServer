
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvLogFileClient : OmvServicesClient
    {
        public OmvLogFileClient(IRpcClient client) : base(client)
        {
            ServiceName = "LogFile";
        }

        //logfile
        //Auth =>  Authentication
        //Boot
        //daemon
        //proftpd =>ftp
        //proftpd_xferlog => proftpd_xferlog 
        public Task<ResponseArray<LogLine>> GetList(string logfile = "syslog")
        {

            var request = NewRequest("getList");
            request.Params = new {id=logfile, start=0,limit=50,sortfield="rownum",sortdir="DESC"};

            var result = RunCmd<ResponseArray<LogLine>>(request);
            return result;
        }

    }
}