using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Helpers;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std.Lists;
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
        public ObservableRangeCollection<string> ThemeOptions { get; } = new ObservableRangeCollection<string>();
        private readonly IRewardService _rewardService;
        private bool _canUseFingerprint;
        private DateTime _endReward;
        private bool _isRewarded;
        private readonly IMessageBoxService _messageBoxService;
        private bool _useFingerprint;
        public ICommand ManageHostsCmd { get; }
        public ICommand OnSwitchChangingCmd { get; }
        public SettingsPAgeViewModel(IRewardService rewardService, IMessageBoxService messageBoxService)
        {
            _rewardService = rewardService;
            _rewardService.OnRewarded += Current_OnRewarded;
            _rewardService.OnRewardedVideoAdFailedToLoad += RewardServiceOnOnRewardedVideoAdFailedToLoad;
            _messageBoxService = messageBoxService;
            ManageHostsCmd = new Command(ManageHosts);
            OnSwitchChangingCmd = new Command(async()=> await OnSwitchChanging());

            ThemeOptions.Add(nameof(Theme.Light));
            ThemeOptions.Add(nameof(Theme.Dark));
            if (ThemeHelper.HasDefaultThemeOption)
                ThemeOptions.Insert(0, "Device Default");
            if (ThemeOptions.Count == 3)
                SelectedTheme = (int)ThemeHelper.CurrentTheme;
            else
                SelectedTheme = (int)ThemeHelper.CurrentTheme - 1;
        }

        private int _selectedTheme;

        public int SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    if (ThemeOptions.Count == 3)
                        ThemeHelper.CurrentTheme = (Theme)value;
                    else
                        ThemeHelper.CurrentTheme = (Theme)(value + 1);

                    ThemeHelper.ChangeTheme(ThemeHelper.CurrentTheme, true);
                }
            }
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
            set => SetProperty(ref _useFingerprint, value);

            //set => SetProperty(ref _useFingerprint, value, null,  (currentValue, newValue) => ProveYouHaveFingers());
            //set => SetProperty(ref _useFingerprint, value, async () => await ProveYouHaveFingers(), null);
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

        private async Task OnSwitchChanging()
        {
            try
            {
                await UseFingerprintAction(!_useFingerprint);
            }
            catch (Exception ex)
            {
                var toto = ex.Message;
            }
        }
        private async Task UseFingerprintAction(bool value)
        {
            var result =await ProveYouHaveFingers();
            if (result)
            {
                _useFingerprint = value;
                Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);
                OnPropertyChanged(nameof(UseFingerprint));
            }
        }
        private async Task<bool> ProveYouHaveFingers(bool retry = true)
        {
            //CrossFingerprint.Current.
            
            var task = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!"));
           
          
            if (task.Authenticated)
            {
                //Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);
            }
            else if (retry) await ProveYouHaveFingers(false);

            return task.Authenticated;
        }
    }
}