using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Doods.Framework.ApiClientBase.Std.Classes
{
    public interface IAddressLookupService
    {
        Task<(IPAddress, string)> GetIpAndName(string hostname);
        Task<PhysicalAddress> GetMac(IPAddress ip);
    }
}