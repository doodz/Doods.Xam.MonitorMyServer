using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Openmedivault.Ssh.Std.Requests;
using Renci.SshNet.Security.Cryptography;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{

    public interface IOMVSshService
    {
        Task<IEnumerable<Devices>> GetDevices();
        Task<IEnumerable<Filesystems>> GetFilesystems();

        Task<IEnumerable<ServicesStatus>> GetServicesStatus();

        Task<IEnumerable<Upgraded>> GetUpgraded();
        Task<IEnumerable<SystemInformation>> GetSystemInformation();

        Task<bool> IsRunning(string filename);
        Task<T> RunCmd<T>(ISshRequest request);
        Task<string> UpdateAptList();
    }

    public class OMVSshService : SshService ,IOMVSshService
    {
        public OMVSshService(ILogger logger, IMapper mapper) : base(logger, mapper)
        {
        }

        public async Task<IEnumerable<Devices>> GetDevices()
        {
            var result = await RunCmd<ResponseArray<Devices>>(new DevicesRequest()).ConfigureAwait(false);
            return result.Data;

        }

        public Task<IEnumerable<Filesystems>> GetFilesystems()
        {
            return RunCmd<IEnumerable<Filesystems>>(new FilesystemsRequest());

        }

        public async Task<IEnumerable<ServicesStatus>> GetServicesStatus()
        {
            var result = await RunCmd<ResponseArray<ServicesStatus>>(new ServicesStatusRequest()).ConfigureAwait(false);
            return result.Data;
        }

        public Task<IEnumerable<Upgraded>> GetUpgraded()
        {
            return RunCmd<IEnumerable<Upgraded>>(new EnumerateUpgradedRequest());

        }

        public new Task<string> UpdateAptList()
        {
            return RunCmd<string>(new UpdateAptListRequest());

        }

        public Task<IEnumerable<SystemInformation>> GetSystemInformation()
        {
            return RunCmd<IEnumerable<SystemInformation>>(new SystemInformationRequest());

        }

        public async Task<bool> IsRunning(string filename)
        {
            var result =  await RunCmd<IsRunning>(new IsRunningRequest(filename)).ConfigureAwait(false);
            return result.Running;
        }


        public async Task<T> RunCmd<T>(ISshRequest request)
        {
            var isRunningPidResult = await ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            return isRunningPidResult.Data;
        }
    }
}