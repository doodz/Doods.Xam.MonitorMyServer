using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;

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
        //messages
        //rsync => Rsync - Jobs
        //rsyncd => Rsync - Server
        //smartd =>S.M.A.R.T.
        //smbdaudit =>SMB/CIFS - Audit
        //syslog => Syslog
        //apt_history => Update Management - History
        //apt_term => Update Management - Terminal output
        /// <summary>
        /// </summary>
        /// <param name="logfile">OmvLogFileEnum</param>
        /// <returns></returns>
        public async Task<ResponseArray<LogLine>> GetList(OmvLogFileEnum logfile = OmvLogFileEnum.syslog)
        {
            var request = NewRequest("getList");
            request.Params = new
                {id = logfile.ToString(), start = 0, limit = 50, sortfield = "rownum", sortdir = "DESC"};

            var result = await RunCmd<ResponseArray<LogLine>>(request);
            return result;
        }
    }
}