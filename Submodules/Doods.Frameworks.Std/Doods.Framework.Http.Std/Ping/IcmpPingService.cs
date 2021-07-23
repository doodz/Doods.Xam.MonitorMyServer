using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;

namespace Doods.Framework.Http.Std.Ping
{
    public class IcmpPingService : DnsPingServiceBase
    {
        public IcmpPingService(IAddressLookupService addressLookupService) : base(
            addressLookupService)
        {
        }

        public override async Task<bool> IsReachable(IPAddress ip, TimeSpan timeout)
        {
            using (var ping = new System.Net.NetworkInformation.Ping())
            {
                var reply = await ping.SendPingAsync(ip, (int) timeout.TotalMilliseconds).ConfigureAwait(false);
                return reply.Status == IPStatus.Success;
            }
        }
    }
}