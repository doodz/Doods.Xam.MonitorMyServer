using System;
using System.Net;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;

namespace Doods.Framework.Http.Std.Ping
{
    public class PingService : DnsPingServiceBase
    {
        private readonly IcmpPingService icmpPing;
        private readonly RdpPortPingService rdpPing;

        public PingService(IAddressLookupService addressLookupService, RdpPortPingService rdpPing,
            IcmpPingService icmpPing) : base(addressLookupService)
        {
            this.rdpPing = rdpPing;
            this.icmpPing = icmpPing;
        }

        public override async Task<bool> IsReachable(IPAddress ip, TimeSpan timeout)
        {
            // start pings in parallel
            var rdp = rdpPing.IsReachable(ip, timeout);
            var icmp = icmpPing.IsReachable(ip, timeout);

            var finished = await Task.WhenAny(rdp, icmp).ConfigureAwait(false);
            // if one finishes early and can connect, return early
            if (await finished.ConfigureAwait(false))
                return true;

            // otherwise wait for the second task and check if it is true
            if (finished == icmp && await rdp.ConfigureAwait(false))
                return true;
            if (finished == rdp && await icmp.ConfigureAwait(false))
                return true;

            return false;
        }
    }
}