using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Http.Std.Ping
{
    public class RdpPortPingService : DnsPingServiceBase
    {
        public RdpPortPingService(IAddressLookupService addressLookupService) : base(addressLookupService)
        {
        }

        private static async Task<bool> IsPortOpen(IPAddress ip, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(ip, port).TimeoutAfter(timeout).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
                when (ex is SocketException || ex is ObjectDisposedException || ex is OperationCanceledException)
            {
                return false;
            }
        }

        public override Task<bool> IsReachable(IPAddress ip, TimeSpan timeout)
        {
            return IsPortOpen(ip, 3389, timeout);
        }
    }
}