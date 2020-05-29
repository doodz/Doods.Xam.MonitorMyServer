using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Enums;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.SelectService
{
    [QueryProperty(nameof(CacheKeyQuery), nameof(CacheKeyQuery))]
    [QueryProperty(nameof(TypeNameQuery), nameof(TypeNameQuery))]
    internal class SelectSupportedServicieViewModel : ViewModel, IQueryShellNavigationObject
    {
        public string CacheKey;
        public Type TypeName;

        public string CacheKeyQuery
        {
            set => CacheKey = value;
        }

        public string TypeNameQuery
        {
            set => TypeName = Type.GetType(value);
        }

        public ICommand GoToLoginCommand { get; }

        public ObservableRangeCollection<ServiceDescription> Items { get; } =
            new ObservableRangeCollection<ServiceDescription>();

        private readonly IConfiguration _config;

        public SelectSupportedServicieViewModel(IConfiguration config)
        {
            _config = config;
            GoToLoginCommand = new Command(GoToLogin);
        }


       

        private void GoToLogin(object obj)
        {
            if (obj is ServiceDescription item)
            {
                var cache = App.Container.Resolve<ISynchronizedCache<object>>();
                var myobject = cache.Read(CacheKey);
                _query = $"TypeServiceQuery={item.Type}";

                if (myobject is ZeroconfHost zeroHost)
                {
                    _query += 
                    $"&DisplayNameQuery={Uri.EscapeDataString(zeroHost.DisplayName)}&PortQuery={item.Port}";


                    if(item.Type == SupportedServicies.Openmediavault_HTTP)
                        _query += $"&IPAddressQuery={Uri.EscapeDataString($"http://{zeroHost.IPAddress}")}";
                    else if(item.Type == SupportedServicies.Openmediavault_HTTPS)
                        _query += $"&IPAddressQuery={Uri.EscapeDataString($"https://{zeroHost.IPAddress}")}";
                    else
                        _query += $"&IPAddressQuery={Uri.EscapeDataString($"{zeroHost.IPAddress}")}";

                }
                else if (myobject is IQueryShellNavigationObject query)
                {
                    _query += "&";
                    _query += query.ToQuery();
                }

                
                if (item.Type == SupportedServicies.Synology) _query += $"&PortQuery={item.Port}";


                var navigationService = App.Container.ResolveKeyed<INavigationService>(App.NavigationServiceType);
                navigationService.NavigateAsync(nameof(LogInPage), this);
            }
        }

        protected override async Task OnInternalAppearingAsync()
        {
            Items.Clear();
            var cache = App.Container.Resolve<ISynchronizedCache<object>>();
            var myobject = cache.Read(CacheKey);
            if (myobject is ZeroconfHost query)
            {
                var item = new ServiceDescription();
           

                if (query.Services.Any(s => s.Key.Contains("_ssh",StringComparison.InvariantCultureIgnoreCase)))
                {

                    var service = query.Services.First(s => s.Key.Contains("_ssh", StringComparison.InvariantCultureIgnoreCase));

                    item.Type = SupportedServicies.Unix;
                    item.Title = "Linux like Debian, Raspbian or Ubuntu";
                    item.Description = "Ssh only";
                    item.Port = service.Value.Port;
                    Items.Add(item);

                    item = new ServiceDescription();
                    item.Type = SupportedServicies.Openmediavault_SSH;
                    item.Title = "Openmediavault";
                    item.Description = "Ssh (more features)";
                    item.Port = service.Value.Port;
                    Items.Add(item);
                }


                if (query.Services.Any(s => s.Key.Contains("_http.",StringComparison.InvariantCultureIgnoreCase)))
                {
                    var service = query.Services.First(s => s.Key.Contains("_http.", StringComparison.InvariantCultureIgnoreCase));
                    item = new ServiceDescription();
                    item.Type = SupportedServicies.Openmediavault_HTTP;
                    item.Title = "Openmediavault";
                    item.Port = service.Value.Port;
                    item.Description = "Http";
                    Items.Add(item);
                    if (!_config.ModeOmvOnlyKey)
                    {
                        item = new ServiceDescription();
                        item.Type = SupportedServicies.Synology;
                        item.Title = "Synology";
                        item.Port = service.Value.Port;
                        item.Description = "Http only";
                        Items.Add(item);
                    }
                }


                if (query.Services.Any(s => s.Key.Contains("_https", StringComparison.InvariantCultureIgnoreCase)))
                {
                    var service = query.Services.First(s => s.Key.Contains("_https", StringComparison.InvariantCultureIgnoreCase));
                    item = new ServiceDescription();
                    item.Type = SupportedServicies.Openmediavault_HTTPS;
                    item.Title = "Openmediavault";
                    item.Port = service.Value.Port;
                    item.Description = "Https";
                    Items.Add(item);

                    
                }
                
            }
            else
                SetDefaultItems();
            await base.OnInternalAppearingAsync();
        }


        private void SetDefaultItems()
        {
            var item = new ServiceDescription();

            item.Type = SupportedServicies.Unix;
            item.Title = "Linux like Debian, Raspbian or Ubuntu";
            item.Port = 22;
            item.Description = "Ssh only";
            Items.Add(item);


            item = new ServiceDescription();
            item.Type = SupportedServicies.Openmediavault;
            item.Title = "Openmediavault";
            item.Port = 443;
            item.Description = "Ssh (more features) or Http";
            Items.Add(item);
            if (!_config.ModeOmvOnlyKey)
            {
                item = new ServiceDescription();
                item.Type = SupportedServicies.Synology;
                item.Title = "Synology";
                item.Port = 5001;
                item.Description = "Http only";
                Items.Add(item);
            }
        }
        private string _query;

        public string ToQuery()
        {
            return _query;
        }
    }
}