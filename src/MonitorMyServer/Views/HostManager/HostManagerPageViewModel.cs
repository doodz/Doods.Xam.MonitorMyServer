using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Http.Std.Ping;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;
using Resource = Doods.Xam.MonitorMyServer.Resx.Resource;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostManagerPageViewModel : DataTableItemsViewModel<Host>
    {
        public ObservableRangeCollection<HostViewModel> ItemsView { get; } = new ObservableRangeCollection<HostViewModel>();
        private ICommand FindItemCommand;
        public HostManagerPageViewModel()
        {
            Title = Resource.Hosts;
            FindItemCommand = new Command(() => NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage)));
        }


        protected override void AddItem(object obj)
        {
            NavigationService.NavigateAsync(nameof(LogInPage));
        }

        protected override void EditItem(object obj)
        {
            if (obj == null) return;
            if (obj is Host h) NavigationService.NavigateAsync(nameof(LogInPage), new DataHostWrapper(h));
        }


        protected override void OnFinishLoading(LoadingContext context)
        {
            if (!Items.Any())
                Title = Resource.NoHostsDetected;
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {

            var lst =base.GetToolBarItemDescriptions().ToList();
            var image1 = SvgIconTarget.CheckCircle.ResourceFile;
            var image3 = new FileImageSource();
            image3.File = image1;
          var cmd = new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Find",
                IsPrimary = true,
                Command = FindItemCommand,
                Icon = image3
            };
            lst.Add(cmd);
            return lst;
        }

        protected override async Task RefreshData()
        {
           await  base.RefreshData();

           ItemsView.ReplaceRange(Items.Select( h => new HostViewModel(h) ));

           foreach (var hostViewModel in ItemsView)
           {
               await hostViewModel.GetMacString();
           }
        }
    }
    public enum HostStatus
    {
        Loading,
        Online,
        Unreachable,
        HostnameInvalid,
        NoHostname
    }
    public static class PingServiceExtensions
    {
        public static HostStatus ToHostStatus(this PingResult result)
        {

            if (result == PingResult.Success) return HostStatus.Online;
            if (result == PingResult.Unreachable) return HostStatus.Unreachable;
            if (result == PingResult.HostNotFound) return HostStatus.HostnameInvalid;
            return HostStatus.Unreachable;

           
        }
    }
    //public static class HostExtensions
    //{
    //    public static string GetMacString(this Host value)
    //    {
    //        var adrBytes = value?.MacAddress ?? throw new ArgumentNullException(nameof(value));
    //        return string.Join(":", from z in adrBytes select z.ToString("X2", CultureInfo.InvariantCulture));
    //    }
    //}
    public class HostViewModel : NotifyPropertyChangedBase
    {
        private IPingService PingService = App.Container.Resolve<IPingService>();
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
           
           
        }

        public async Task GetMacString()
        {
            var toto = new AddressLookupService();
            var ip =await toto.GetIpAndName(Url);
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
            get { return _statut;}
            set { SetProperty(ref _statut, value); }
        }
        public string MacAddress
        {
            get { return _macAddress; }
            set { SetProperty(ref _macAddress, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get;  }

        /// <summary>
        /// 
        /// </summary>
        public string HostName { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get;  }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get;  }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get;  }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOmvServer { get;  }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSynoServer { get;  }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRpi { get;  }
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