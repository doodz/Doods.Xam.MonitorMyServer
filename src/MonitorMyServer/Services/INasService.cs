using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Data.Nas;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface INasService
    {
        Task<IEnumerable<SharedFolder>> GetSharedFolders();
        //Task<IEnumerable<FileSystem>> GetFileSystems();

    }

    public interface IPackageUpdates
    {
        Task<IEnumerable<Package>> GetPackages();
    }
}