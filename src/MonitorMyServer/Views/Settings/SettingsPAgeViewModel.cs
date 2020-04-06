using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using MarcTron.Plugin.CustomEventArgs;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Settings
{
    public class SettingsPAgeViewModel : ViewModel
    {
        private readonly IRewardService _rewardService;
        private bool _canUseFingerprint;
        private DateTime _endReward;
        private bool _isRewarded;
        private readonly IMessageBoxService _messageBoxService;
        private bool _useFingerprint;
        public ICommand ManageHostsCmd { get; }

        public SettingsPAgeViewModel(IRewardService rewardService, IMessageBoxService messageBoxService)
        {
            _rewardService = rewardService;
            _rewardService.OnRewarded += Current_OnRewarded;
            _rewardService.OnRewardedVideoAdFailedToLoad += RewardServiceOnOnRewardedVideoAdFailedToLoad;
            _messageBoxService = messageBoxService;
            ManageHostsCmd = new Command(ManageHosts);
        }
        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
        }   
        public ICommand ShowRewarVideoCmd => new Command(ShowRewardVideo);

        public bool IsRewarded
        {
            get => _isRewarded;
            private set => SetProperty(ref _isRewarded, value);
        }

        public DateTime EndReward
        {
            get => _endReward;
            private set => SetProperty(ref _endReward, value);
        }


        public bool UseFingerprint
        {
            get => _useFingerprint;
            set => SetProperty(ref _useFingerprint, value, async () => await ProveYouHaveFingers(), null);
        }

        public bool CanUseFingerprint
        {
            get => _canUseFingerprint;
            private set => SetProperty(ref _canUseFingerprint, value);
        }

        private void RewardServiceOnOnRewardedVideoAdFailedToLoad(object sender, MTEventArgs e)
        {
#if DEBUG

            _messageBoxService.ShowAlert("Can't load",
                $"{e} - {e.ErrorCode} - {e.RewardAmount} - {e.RewardType}");
#endif
        }

        private void Current_OnRewarded(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void ShowRewardVideo()
        {
            _rewardService.ShowRewardedVideo();
        }

        protected override async Task OnInternalAppearingAsync()
        {
            CanUseFingerprint = await CrossFingerprint.Current.IsAvailableAsync();
            await RefreshData();
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData(bool b = true)
        {
            //Xamarin.Essentials.Preferences
            UseFingerprint = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            EndReward = _rewardService.EndReward;
            IsRewarded = _rewardService.IsRewarded;
            return Task.FromResult(0);
        }

        private async Task ProveYouHaveFingers(bool retry = true)
        {
            //CrossFingerprint.Current.

            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!"));
            if (result.Authenticated)
                Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);
            else if (retry) await ProveYouHaveFingers(false);

            await Task.FromResult(0);
        }
    }
}