using System;
using System.Net;
using System.Threading.Tasks;

namespace Doods.Framework.ApiClientBase.Std.Classes
{
    public interface IPingService
    {
        Task<bool> IsReachable(IPAddress ip, TimeSpan timeout);
        Task<PingResult> IsReachable(string hostname, TimeSpan timeout);

        // public  PingResult;
    }
}