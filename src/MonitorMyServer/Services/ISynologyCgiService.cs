using Doods.Synology.Webapi.Std;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface ISynologyCgiService: INasService,ISynoInfoClient, ISynoAuthClient, ISynoSystemClient,ISynoUpgradeClient, ISynoStorageClient, ISynoShareClient, ISynoServiceClient, ISynoPackageClient
    {
         
    }
}