using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Converters;
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
        Task<IEnumerable<Process>> GetProcesses();
        Task<IEnumerable<string>> GetInterfaces();
        Task<bool> UpdateAptList();
        Task<int> NoUpCommand(string cmd);
        Task<int> InstallPackage(IEnumerable<string> packagesName);
        Task<int> InstallAllPackage();
        Task<bool> IsRunning(int pid);
        Task<double> GetUptimeDouble();
        Task<TimeSpan> GetUptime();
        Task<string> GetUptimeString();
        Task<MemoryUsage> CheckMemoryUsage();
        Task<string> RunCommand(string cmd);
        Task Rebout();
        Task Halt();
    }


    public class SshService : SshServiceBase, ISshService
    {
        private IMapper _mapper;
        public SshService(ILogger logger,IMapper mapper):base(logger)
        {
            _mapper = mapper;
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
            var upgradables = _mapper.Map<IEnumerable<UpgradableBean>, IEnumerable<Upgradable>>(upgradableBean.Data);
            return upgradables;

        }
        public async Task<IEnumerable<Process>> GetProcesses()
        {
            var processesRequest = new ProcessesRequest();
            var processBeans = await ExecuteTaskAsync<IEnumerable<ProcessBean>>(processesRequest).ConfigureAwait(false);
            var processs = _mapper.Map<IEnumerable<ProcessBean>, IEnumerable<Process>>(processBeans.Data);
            return processs;
        }
        public async Task<MemoryUsage> CheckMemoryUsage()
        {
            var memoryUsageRequest = new MemoryUsageRequest();
            var osMemoryBean = await ExecuteTaskAsync<OsMemoryBean>(memoryUsageRequest).ConfigureAwait(false);
            var memoryUsage = _mapper.Map<OsMemoryBean, MemoryUsage>(osMemoryBean.Data);
            return memoryUsage;
        }
        public async Task<int> NoUpCommand(string cmd)
        {
            var noUpRequest = new NoHupRequest(cmd);
            var noUpBean = await ExecuteTaskAsync<int>(noUpRequest).ConfigureAwait(false);
            return noUpBean.Data;

        }

        public async Task<string> RunCommand(string cmd)
        {
            var noUpRequest = new CustomRequest(cmd);
            var noUpBean = await ExecuteTaskAsync<string>(noUpRequest).ConfigureAwait(false);
            return noUpBean.Data;

        }

        public async Task Rebout()
        {
           var reboutRequest = new RebootRequest(true);
           var result =await ExecuteTaskAsync<string>(reboutRequest).ConfigureAwait(false);
        }

        public async Task Halt()
        {
           var haltRequest = new HaltSignalRequest(true);
           var result = await ExecuteTaskAsync<string>(haltRequest).ConfigureAwait(false);
        }

        public async Task<CpuInfo> GetCpuInfo()
        {
            var cpuInfoRequest = new CpuInfoRequest();
            var cpuInfoBean = await ExecuteTaskAsync<CpuInfoBean>(cpuInfoRequest).ConfigureAwait(false);
            var cpuInfo = _mapper.Map<CpuInfoBean, CpuInfo>(cpuInfoBean.Data);
            return cpuInfo;
        }
        public async Task<IEnumerable<Lastlogin>> GetLastLogins()
        {
            var lastLoginsRequest = new LastLoginsRequest();
            var cpuInfoBean = await ExecuteTaskAsync<IEnumerable<LastloginBean>>(lastLoginsRequest).ConfigureAwait(false);
            var lastLogins = _mapper.Map<IEnumerable<LastloginBean>, IEnumerable<Lastlogin>>(cpuInfoBean.Data);
            return lastLogins;
        }
        
        public async Task<double> GetUptimeDouble()
        {
            var uptimeRequest = new UptimeRequest();
            var uptimeBean = await ExecuteTaskAsync<double>(uptimeRequest).ConfigureAwait(false);
           
            return uptimeBean.Data;
        }

        public async Task<TimeSpan> GetUptime()
        {
            var uptimeRequest = new UptimeRequest();
            var uptimeBean = await ExecuteTaskAsync<TimeSpan>(uptimeRequest).ConfigureAwait(false);
            return uptimeBean.Data;

        }
        public async Task<string> GetUptimeString()
        {
            var uptimeRequest = new UptimeRequest();
            var uptimeBean = await ExecuteTaskAsync<double>(uptimeRequest).ConfigureAwait(false);
            var converter = new DoubleToDateStringConverter();
            return (string)converter.Convert(uptimeBean.Data, null, string.Empty, CultureInfo.CurrentCulture);

        }

        public async Task<IEnumerable<DiskUsage>> GetDisksUsage()
        {
            var diskUsageRequest = new DiskUsageRequest();
            var diskUsageBean = await ExecuteTaskAsync<IEnumerable<DiskUsageBean>>(diskUsageRequest).ConfigureAwait(false);
            var DisksUsage = _mapper.Map<IEnumerable<DiskUsageBean>, IEnumerable<DiskUsage>>(diskUsageBean.Data);
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
