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

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostViewModel : NotifyPropertyChangedBase
    {
        private IPingService PingService = App.Container.Resolve<IPingService>();
        public readonly Host Host;

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

        public async Task GetMacString()
        {
            var toto = new AddressLookupService();
            var ip = await toto.GetIpAndName(Url);
            //var adress= await toto.GetMac(ip.Item1);
            //MacAddress = GetMacString(adress);
            await PingAndSetStatus(ip.Item1);
        }

        private async Task PingAndSetStatus(IPAddress adress)
        {
            var timeout = 2500;
            var res = await PingService.IsReachable(adress, TimeSpan.FromMilliseconds(timeout));
            Status = res;
            //Status = res.ToHostStatus();
            //base.StateHasChanged();
        }

        private string GetMacString(PhysicalAddress adress)
        {
            var adrBytes = adress.GetAddressBytes();
            return string.Join(":", from z in adrBytes select z.ToString("X2", CultureInfo.InvariantCulture));
        }


        private bool _statut;
        private string _macAddress;

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
        /// 
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// 
        /// </summary>
        public string HostName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOmvServer { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynoServer { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRpi { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSsh { get; set; }

        public string Description => $"{UserName}@{Url}:{Port}";

        protected string DebuggerDisplay()
        {
            return
                $"HostName:{HostName})";
        }
    }
}