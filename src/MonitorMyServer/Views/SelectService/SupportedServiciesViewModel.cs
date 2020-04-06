using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
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
        public ObservableRangeCollection<ServiceDescription> Items { get; }= new ObservableRangeCollection<ServiceDescription>();

        public SelectSupportedServicieViewModel()
        {
            GoToLoginCommand = new Command(GoToLogin);
        }

        private void GoToLogin(object obj)
        {
            if (obj is ServiceDescription item)
            {
                var cache = App.Container.Resolve<ISynchronizedCache<object>>();
                var myobject = cache.Read(CacheKey);
                _query = $"TypeServiceQuery={item.Type}";


                if (myobject is IQueryShellNavigationObject query)
                {
                    _query += "&";
                    _query +=query.ToQuery();
                }

                if (item.Type == SupportedServicies.Synology)
                {
                    _query += $"&PortQuery={5001}";

                }
                    

                var navigationService = App.Container.ResolveKeyed<INavigationService>(App.NavigationServiceType);
                navigationService.NavigateAsync(nameof(LogInPage), this);
            }
        }

        protected override async Task OnInternalAppearingAsync()
        {

            var item = new ServiceDescription();
            item.Type = Enums.SupportedServicies.Unix;
            item.Title = "Unix like raspbian or Ubuntu";
            item.Description = "Ssh only";
            Items.Add(item);

            item = new ServiceDescription();
            item.Type = Enums.SupportedServicies.Openmediavault;
            item.Title = "Openmediavault";
            item.Description = "Ssh or Http";
            Items.Add(item);

            item = new ServiceDescription();
            item.Type = Enums.SupportedServicies.Synology;
            item.Title = "Synology";
            item.Description = "Http only";
            Items.Add(item);

            await base.OnInternalAppearingAsync();
        }

        private string _query;
        public string ToQuery()
        {
            return _query;
        }
    }
}
