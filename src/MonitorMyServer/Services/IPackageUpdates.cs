using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Data.Nas;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IPackageUpdates
    {
        Task<IEnumerable<Package>> GetPackages();
        Task UpdatePackages(IEnumerable<Package> packages);
    }
}