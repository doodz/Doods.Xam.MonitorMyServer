using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Mobile.Std.Enums;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvAptClient : OmvServiceClient
    {
        public OmvAptClient(IRpcClient client) : base(client)
        {
            ServiceName = "Apt";
        }

        public Task<AptSetting> GetSettings()
        {
            var request = NewRequest("getSettings");

            var result = RunCmd<AptSetting>(request);
            return result;
        }

        public Task<object> SetSettings(AptSetting settings)
        {
            var request = NewRequest("setSettings");
            request.Params = settings;

            var result = RunCmd<object>(request);
            return result;
        }

        public Task<IEnumerable<Upgraded>> GetUpgraded()
        {
            var request = NewRequest("enumerateUpgraded");

            var result = RunCmd<IEnumerable<Upgraded>>(request);

            return result;
        }

        public Task<string> UpdateAptList()
        {
            var request = NewRequest("update");

            var result = RunCmd<string>(request);

            return result;
        }

        //Form OMV 5
        public Task<string> Install(IEnumerable<string> lst)
        {
            var request = NewRequest("install");
            request.Params = new {packages = lst};

            var result = RunCmd<string>(request);

            return result;
        }


        public async Task<string> InstallPacjages(IEnumerable<string> lst)
        {
            await CheckRpcVersionAsync();
            var request = NewRequest("getInformation");
            request.Options = new Options {Updatelastaccess = false};

            if (GetRpcVersion() < OMVVersions.Version5) return await UpgradeAptList(lst);
            return await Install(lst);
        }

        //For OMV 4
        public Task<string> UpgradeAptList(IEnumerable<string> lst)
        {
            var request = NewRequest("upgrade");
            request.Params = new {packages = lst};

            var result = RunCmd<string>(request);

            return result;
        }
    }
}