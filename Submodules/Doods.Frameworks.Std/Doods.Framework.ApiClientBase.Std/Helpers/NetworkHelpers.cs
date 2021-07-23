using System.Net.Sockets;

namespace Doods.Framework.ApiClientBase.Std.Helpers
{
    public class NetworkHelpers
    {
        public static bool TestRemoteHost(string hostname, int port)
        {
            using (var client = new UdpClient(hostname, port))
            {
                return true;
            }
        }
    }
}