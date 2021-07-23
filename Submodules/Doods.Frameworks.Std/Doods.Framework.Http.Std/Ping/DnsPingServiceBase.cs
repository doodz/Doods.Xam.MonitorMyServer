using System;
using System.Net;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;

namespace Doods.Framework.Http.Std.Ping
{
    public abstract class DnsPingServiceBase : IPingService
    {
        private readonly IAddressLookupService _addressLookupService;

        public DnsPingServiceBase(IAddressLookupService addressLookupService)
        {
            _addressLookupService = addressLookupService;
        }

        public async Task<PingResult> IsReachable(string hostname, TimeSpan timeout)
        {
            IPAddress ip;
            try
            {
                (ip, _) = await _addressLookupService.GetIpAndName(hostname).ConfigureAwait(false);
            }
            // could throw at least SocketException, ArgumentException, ArgumentOutOfRangeException, InvalidOperationException, maybe NRE, which would be of interest
            // we want to catch them all, basically anything that can make the resolution fail

            catch
            {
                return PingResult.HostNotFound;
            }

            return await IsReachable(ip, timeout).ConfigureAwait(false) ? PingResult.Success : PingResult.Unreachable;
        }

        public abstract Task<bool> IsReachable(IPAddress ip, TimeSpan timeout);
    }
}