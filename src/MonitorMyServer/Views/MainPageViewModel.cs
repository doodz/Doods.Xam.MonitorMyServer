using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using System.Threading.Tasks;

namespace Doods.Xam.MonitorMyServer.Views
{
    public class MainPageViewModel : ViewModel
    {
        private ViewModelStateItem _viewModelStateItem;

        public ViewModelStateItem ViewModelStateItem
        {
            get { return _viewModelStateItem;}
            private set { SetProperty(ref _viewModelStateItem, value); }
        }

        private readonly ISshService _sshService;
        public MainPageViewModel(ISshService sshService)
        {
           Title = Resource.Home;
            _sshService = sshService;
        }


        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            var count = await DataProvider.CountHostAsync();

            if (count <= 0)
            {
                ShowErrorHostState();
              
            }

            await Login(count);


        }

        private void ShowErrorHostState()
        {
            var tmp = new ViewModelStateItem(this);


        }

        private async Task Login(int count)
        {
            var con = new SshConnection("192.168.1.73", "pi", "raspberry");
            _sshService.Init(con, true);
            var toto = new toto() { };
            toto.Handler = toto;
            var val = await _sshService.ExecuteTaskAsync<string>(toto);
            var total = $"{count} -- {val.Data}";
        }
    }
}
