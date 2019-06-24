using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using MarcTron.Plugin;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.AptUpdates
{
    public class AptUpdatesPageViewModel : ViewModelWhithState
    {
        
        private readonly IMessageBoxService _messageBoxService;
        private readonly ISshService _sshService;
        private IEnumerable<Upgradable> _upgradables;

        private int _upgradablesCount;

        public ICommand UpdatesCmd { get; }
        public ICommand UpdateSelectedItemsCmd { get; }
        
        public AptUpdatesPageViewModel(ISshService sshService, IMessageBoxService messageBoxService,IConfiguration configuration)
        {
            Title = Resource.Apt;
            _sshService = sshService;
            _messageBoxService = messageBoxService;
            UpdatesCmd = new Command(Updates);
            UpdateSelectedItemsCmd = new Command(UpdateSelectedItems);
            CrossMTAdmob.Current.LoadRewardedVideo(configuration.RewardedVideoKey);
            
        }

        private async void UpdateSelectedItems()
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
            return;
            ViewModelStateItem.IsRunning = true;
            var lst = _upgradables?.Where(u => u.IsSelected).Select(u=>u.Name).ToList();
            if (lst == null || lst.IsEmpty()) return;
            var pId = 0;
            SetLabelsStateItem(Resource.InstallingPackages,string.Format(Resource.ThereAre_0_OfSelected,lst.Count));
            if (lst.Count > 2)
            {
               pId= await _sshService.InstallPackage(lst.Take(2));
            }
            else
               pId = await _sshService.InstallPackage(lst);
            SetLabelsStateItem(Resource.InstallInProgress, string.Format(Resource.ThereAre_0_OfSelected, lst.Count));

            
            _pidStatusChecker =new PidStatusChecker(pId, OnResult, Token);

        }

        private PidStatusChecker _pidStatusChecker;
        private void OnResult(bool result)
        {
            ViewModelStateItem.IsRunning = false;
            if (result)
            {
                SetLabelsStateItem(Resource.InstallInProgress, Resource.NotFinished);
            }
            else
            {
                SetLabelsStateItem(Resource.InstallInProgress, Resource.Done);
            }
        }

        private async void Updates()
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
            return;
            ViewModelStateItem.IsRunning = true;
            var pId = await _sshService.InstallAllPackage();
            SetLabelsStateItem(Resource.InstallingPackages, string.Format(Resource.ThereAre_0_OfSelected, 12));
            _pidStatusChecker = new PidStatusChecker(pId, OnResult, Token);
        }
        public int UpgradablesCount
        {
            get => _upgradablesCount;
            set => SetProperty(ref _upgradablesCount, value);
        }

        public IEnumerable<Upgradable> Upgradables
        {
            get => _upgradables;
            set => SetProperty(ref _upgradables, value);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            SetLabelsStateItem(Resource.PleaseWait, Resource.PacketReading);
            ViewModelStateItem.IsRunning = true;
            //await _sshService.UpdateAptList().ConfigureAwait(false);
            await GetUpgradables();
            ViewModelStateItem.IsRunning = false;
            SetLabelsStateItem(Resource.PleaseWait, string.Format(Resource.ThereAre_0_ItemsCanBeUpdated, UpgradablesCount));
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
        }

        private async Task GetUpgradables()
        {
            Upgradables = await _sshService.GetUpgradables();
            UpgradablesCount = _upgradables.Count();
        }
    }
}