using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std.Extensions;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Doods.Xam.MonitorMyServer.Views.AptUpdates
{
    public class AptUpdatesPageViewModel : ViewModelWhithState
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IRewardService _rewardService;
       
        private PidStatusChecker _pidStatusChecker;
        private IEnumerable<Upgradable> _upgradables;
        private readonly ISshService _sshService;
        private int _upgradablesCount;


        public AptUpdatesPageViewModel(ISshService sshService, IMessageBoxService messageBoxService,
            IRewardService rewardService)
        {
            Title = Resource.Apt;
            _sshService = sshService;
            _messageBoxService = messageBoxService;
            UpdatesCmd = new Command(Updates);
            UpdateSelectedItemsCmd = new Command(UpdateSelectedItems);
            SelectUnselectAllItemsCmd = new Command(SelectUnselectAllItems);

            _rewardService = rewardService;
            _rewardService.OnRewarded += Current_OnRewarded;
            _rewardService.OnRewardedVideoAdClosed += Current_OnRewardedVideoAdClosed;
            _rewardService.OnRewardedVideoAdLeftApplication += Current_OnRewardedVideoAdLeftApplication;
        }

        public ICommand UpdatesCmd { get; }
        public ICommand UpdateSelectedItemsCmd { get; }
        public ICommand SelectUnselectAllItemsCmd { get; }

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

        //protected override async Task OnInternalAppearingAsync()
        //{
        //    Updates();
        //}

        protected override Task OnInternalDisappearingAsync()
        {
            _rewardService.OnRewarded -= Current_OnRewarded;
            _rewardService.OnRewardedVideoAdClosed -= Current_OnRewardedVideoAdClosed;
            _rewardService.OnRewardedVideoAdLeftApplication -= Current_OnRewardedVideoAdLeftApplication;
            return base.OnInternalDisappearingAsync();
        }


        private void Current_OnRewarded(object sender, EventArgs e)
        {
            SetLabelsStateItem("let's go", "Update running!");
        }

        private void Current_OnRewardedVideoAdLeftApplication(object sender, EventArgs e)
        {
            SetLabelsStateItem("Warning", "you need to wath all video");
        }


        private void Current_OnRewardedVideoAdClosed(object sender, EventArgs e)
        {
            if (!_rewardService.IsRewarded)
                SetLabelsStateItem("Warning", "you need to wath all video");
        }

        private async void SelectUnselectAllItems()
        {
            var countSelected = _upgradables.Count(u => u.IsSelected);
            var select = countSelected < _upgradablesCount;
            _upgradables.ForEach(u => u.IsSelected = select);
        }

        private async void UpdateSelectedItems()
        {
            if (!_rewardService.IsRewarded)
            {
                _rewardService.ShowRewardedVideo(UpdateSelectedItems);
                return;
            }

            ViewModelStateItem.IsRunning = true;
            var lst = _upgradables?.Where(u => u.IsSelected).Select(u => u.Name).ToList();
            if (lst == null || lst.IsEmpty()) return;
            var pId = 0;
            SetLabelsStateItem(Resource.InstallingPackages, string.Format(Resource.ThereAre_0_OfSelected, lst.Count));
            pId = await _sshService.InstallPackage(lst);

            _pidStatusChecker = new PidStatusChecker(pId, OnResult, Token);
        }


        //protected override Task in()
        //{
        //    CrossMTAdmob.Current.OnRewarded -= Current_OnRewarded;//When the user gets a reward
        //    CrossMTAdmob.Current.OnRewardedVideoAdClosed -= Current_OnRewardedVideoAdClosed; ;//When the ads is closed
        //    CrossMTAdmob.Current.OnRewardedVideoAdFailedToLoad -= Current_OnRewardedVideoAdFailedToLoad; ;      //When the ads fails to load
        //    CrossMTAdmob.Current.OnRewardedVideoAdLeftApplication -= Current_OnRewardedVideoAdLeftApplication; ; //When the users leaves the application
        //    return base.OnInternalDisappearingAsync();
        //}


        private async Task OnResult(bool result)
        {
            ViewModelStateItem.IsRunning = false;
            if (result)
                SetLabelsStateItem(Resource.InstallInProgress, Resource.NotFinished);
            else
                SetLabelsStateItem(Resource.InstallInProgress, Resource.Done);
            ViewModelStateItem.IsRunning = true;

            await GetUpgradables();
            ViewModelStateItem.IsRunning = false;
        }

        private async void Updates()
        {
            if (!_rewardService.IsRewarded)
            {
                _rewardService.ShowRewardedVideo(Updates);
                return;
            }

            ViewModelStateItem.IsRunning = true;
            var pId = await _sshService.InstallAllPackage();
            SetLabelsStateItem(Resource.InstallingPackages, Resource.UpdatesAll);
            _pidStatusChecker = new PidStatusChecker(pId, OnResult, Token);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            SetLabelsStateItem(Resource.PleaseWait, Resource.PacketReading);
            ViewModelStateItem.IsRunning = true;
            //await _sshService.UpdateAptListRequest().ConfigureAwait(false);
            await GetUpgradables();
            ViewModelStateItem.IsRunning = false;
            SetLabelsStateItem(Resource.PleaseWait,
                string.Format(Resource.ThereAre_0_ItemsCanBeUpdated, UpgradablesCount));
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