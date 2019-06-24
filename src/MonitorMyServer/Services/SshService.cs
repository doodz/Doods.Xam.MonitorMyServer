using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Framework.Ssh.Std.Requests;
using Doods.Framework.Ssh.Std.Requests.YetRequest;
using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface ISshService:IClientSsh
    {
        void Init(IConnection connection, bool andConnect);
        Task InitAsync(IConnection connection, bool andConnect = true);
        Task<CpuInfo> GetCpuInfo();
        Task<IEnumerable<Upgradable>> GetUpgradables();
        Task<IEnumerable<DiskUsage>> GetDisksUsage();
        Task<IEnumerable<string>> GetInterfaces();
        Task<bool> UpdateAptList();
        Task<int> NoUpCommand(string cmd);
        Task<int> InstallPackage(IEnumerable<string> packagesName);
        Task<int> InstallAllPackage();
        Task<bool> IsRunning(int pid);
    }
    public class SshService : SshServiceBase, ISshService
    {

        public SshService(ILogger logger):base(logger)
        {
            
        }

        public void Init(IConnection connection,bool andConnect = true)
        {
            Connection = connection;
            if (andConnect)
                Connect();
        }


        public async Task InitAsync(IConnection connection, bool andConnect = true)
        {
            Connection = connection;
            if (andConnect)
                await ConnectAsync();
        }


        public async Task<bool> IsRunning(int pid)
        {
            var isRunningPidRequest = new IsRunningPidRequest(pid);
            var isRunningPidResult = await ExecuteTaskAsync<bool>(isRunningPidRequest).ConfigureAwait(false);
            return isRunningPidResult.Data;
        }

        private async Task<ISshResponse<int>> RunInNoHup(ISshRequest request)
        {
            var noUpREquest = new NoHupRequest(request);
            var pid = await ExecuteTaskAsync<int>(noUpREquest).ConfigureAwait(false);
            return pid;
        }

        public async Task<int> InstallPackage(IEnumerable<string> packagesName)
        {
            if (packagesName == null) throw new NullReferenceException(nameof(packagesName));
            var request = new InstallPackagesRequest(packagesName);
            var result = await RunInNoHup(request);
    
            return result.Data;
        }

        public async Task<int> InstallAllPackage()
        {
            var request = new UpgradeAllRequest();
            var result = await RunInNoHup(request);
            return result.Data;
        }

        public async Task<IEnumerable<Upgradable>> GetUpgradables()
        {
            var upgradableRequest = new UpgradableRequest();
            var upgradableBean = await ExecuteTaskAsync<IEnumerable<UpgradableBean>>(upgradableRequest).ConfigureAwait(false);
            var upgradables = Mapper.Map<IEnumerable<UpgradableBean>, IEnumerable<Upgradable>>(upgradableBean.Data);
            return upgradables;

        }

        public async Task<int> NoUpCommand(string cmd)
        {
            var noUpRequest = new NoHupRequest(cmd);
            var noUpBean = await ExecuteTaskAsync<int>(noUpRequest).ConfigureAwait(false);
            return noUpBean.Data;

        }

        public async Task<CpuInfo> GetCpuInfo()
        {
            var cpuInfoRequest = new CpuInfoRequest();
            var cpuInfoBean = await ExecuteTaskAsync<CpuInfoBean>(cpuInfoRequest).ConfigureAwait(false);
            var CpuInfo = Mapper.Map<CpuInfoBean, CpuInfo>(cpuInfoBean.Data);
            return CpuInfo;
        }


        public async Task<IEnumerable<DiskUsage>> GetDisksUsage()
        {
            var diskUsageRequest = new DiskUsageRequest();
            var diskUsageBean = await ExecuteTaskAsync<IEnumerable<DiskUsageBean>>(diskUsageRequest).ConfigureAwait(false);
            var DisksUsage = Mapper.Map<IEnumerable<DiskUsageBean>, IEnumerable<DiskUsage>>(diskUsageBean.Data);
            return DisksUsage;
        }


        public async Task<IEnumerable<string>> GetInterfaces()
        {
            var interfaceRequest = new InterfacesRequest();
            var interfaces = await ExecuteTaskAsync<IEnumerable<string>>(interfaceRequest).ConfigureAwait(false);
            return interfaces.Data;
        }


        public async Task<bool> UpdateAptList()
        {
            var aptUpdateRequest = new AptUpdateRequest(true);
            var result = await ExecuteTaskAsync<string>(aptUpdateRequest).ConfigureAwait(false);

            if (result.ResponseStatus == ResponseStatus.Error)
            {
                throw new SshExitCodeCommandException(result.ErrorMessage, result.StatusCode);
            }

            return result.ResponseStatus == ResponseStatus.Completed;
           
        }
    }
}
