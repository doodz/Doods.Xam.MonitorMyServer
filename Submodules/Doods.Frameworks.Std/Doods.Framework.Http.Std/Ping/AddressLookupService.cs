using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;

namespace Doods.Framework.Http.Std.Ping
{
    public class AddressLookupService : IAddressLookupService
    {
        public async Task<(IPAddress, string)> GetIpAndName(string hostname)
        {
            var localname = hostname;
            if (localname.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                localname = localname.Substring(8);
            else if (localname.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                localname = localname.Substring(7);

            var res = await Dns.GetHostEntryAsync(localname).ConfigureAwait(false);
            return (res.AddressList.First(ip => ip.AddressFamily == AddressFamily.InterNetwork), res.HostName);
        }

        public async Task<PhysicalAddress> GetMac(IPAddress ip)
        {
            return await DoodsPingService.LookupAsync(ip).ConfigureAwait(false);
        }
    }
}