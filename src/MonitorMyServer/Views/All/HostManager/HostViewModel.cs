using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Http.Std.Ping;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostViewModel : NotifyPropertyChangedBase
    {
        private readonly IHistoryService _historyService = App.Container.Resolve<IHistoryService>();
        private readonly IPingService _pingService = App.Container.Resolve<IPingService>();

        public readonly Host Host;

        private DateTime _lastSync;
        private DateTime _lastUpdate;
        private string _macAddress;
        private int _nombrerPackargeCanBeUpdted;

        private bool _statut;

        public HostViewModel(Host host)
        {
            Port = host.Port;
            HostName = host.HostName;
            Url = host.Url;
            UserName = host.UserName;
            Password = host.Password;
            IsOmvServer = host.IsOmvServer;
            IsSynoServer = host.IsSynoServer;
            IsRpi = host.IsRpi;
            IsSsh = host.IsSsh;
            Host = host;
        }

        public DateTime LastSync
        {
            get => _lastSync;
            private set => SetProperty(ref _lastSync, value);
        }

        public DateTime LastUpdate
        {
            get => _lastUpdate;
            private set => SetProperty(ref _lastUpdate, value);
        }

        public int NombrerPackargeCanBeUpdted
        {
            get => _nombrerPackargeCanBeUpdted;
            private set => SetProperty(ref _nombrerPackargeCanBeUpdted, value);
        }

        public bool Status
        {
            get => _statut;
            set => SetProperty(ref _statut, value);
        }

        public string MacAddress
        {
            get => _macAddress;
            set => SetProperty(ref _macAddress, value);
        }

        /// <summary>
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// </summary>
        public string HostName { get; }

        /// <summary>
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// </summary>
        public bool IsOmvServer { get; }

        /// <summary>
        /// </summary>
        public bool IsSynoServer { get; }

        /// <summary>
        /// </summary>
        public bool IsRpi { get; }

        /// <summary>
        /// </summary>
        public bool IsSsh { get; set; }

        public string Description => $"{UserName}@{Url}:{Port}";

        public async Task GetMacString()
        {
            var task = ReadHistoryFileAsync();
            var toto = new AddressLookupService();
            var ip = await toto.GetIpAndName(Url);
            //var adress= await toto.GetMac(ip.Item1);
            //MacAddress = GetMacString(adress);
            await PingAndSetStatus(ip.Item1);
            await task;
        }


        private async Task ReadHistoryFileAsync()
        {
            await _historyService.GetHistoryAsync(Host.Id.Value).ContinueWith(task1 =>
            {
                if (task1.Status == TaskStatus.RanToCompletion)
                {
                    Task.Delay(500);
                    LastSync = task1.Result.LastSync;
                    Task.Delay(500);
                    LastUpdate = task1.Result.LastUpdate;
                    Task.Delay(500);
                    NombrerPackargeCanBeUpdted = task1.Result.NombrerPackargeCanBeUpdted;
                }
                else if (task1.Status == TaskStatus.Faulted)
                {
                    var ex = task1.Exception.Message;
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
        }
        private async Task PingAndSetStatus(IPAddress adress)
        {
            var timeout = 2500;
            var res = await _pingService.IsReachable(adress, TimeSpan.FromMilliseconds(timeout));
            Status = res;
            //Status = res.ToHostStatus();
            //base.StateHasChanged();
        }

        private string GetMacString(PhysicalAddress adress)
        {
            var adrBytes = adress.GetAddressBytes();
            return string.Join(":", from z in adrBytes select z.ToString("X2", CultureInfo.InvariantCulture));
        }

        protected string DebuggerDisplay()
        {
            return
                $"HostName:{HostName})";
        }
    }
}