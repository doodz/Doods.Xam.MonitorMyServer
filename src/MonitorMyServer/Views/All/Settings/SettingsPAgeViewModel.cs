﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Helpers;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using MarcTron.Plugin.CustomEventArgs;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Settings
{
    public class SettingsPAgeViewModel : ViewModel
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IRewardService _rewardService;
        private bool _canUseFingerprint;
        private DateTime _endReward;
        private bool _isRewarded;
        public LayoutState CurrentState => LayoutState.Custom;
        private int _selectedTheme;
        public ICommand ItemTappedCommand { get; }
        private bool _useFingerprint;
        public string _customState;
        public string CustomState
        {
            get => _customState;
            set => SetProperty(ref _customState, value);
        }
        //public ICommand OnSwitchChangingCmd { get; }
        public SettingsPAgeViewModel(IRewardService rewardService, IMessageBoxService messageBoxService)
        {
            _useFingerprint = false;
            _rewardService = rewardService;
            _rewardService.OnRewarded += Current_OnRewarded;
            _rewardService.OnRewardedVideoAdFailedToLoad += RewardServiceOnOnRewardedVideoAdFailedToLoad;
            _messageBoxService = messageBoxService;
            ManageHostsCmd = new Command(ManageHosts);
            //OnSwitchChangingCmd = new Command(async()=> await OnSwitchChanging());
            ItemTappedCommand = new Command<string>(a=>ExecuteItemTappedCommand(a));
            ThemeOptions.Add(nameof(Theme.Light));
            ThemeOptions.Add(nameof(Theme.Dark));
            if (ThemeHelper.HasDefaultThemeOption)
                ThemeOptions.Insert(0, "Device Default");
            else
            {
                CustomState = "NoDefault";
            }

            if (ThemeOptions.Count == 3)
                SelectedTheme = (int) ThemeHelper.CurrentTheme;
            else
                SelectedTheme = (int) ThemeHelper.CurrentTheme - 1;
        }

         void ExecuteItemTappedCommand(string a)
        {

            if (Islight)
            {
                SelectedTheme = (int)Theme.Light;
            }
            if (IsDark)
            {
                SelectedTheme = (int)Theme.Dark;
            }
            if (IsDefault)
            {
                SelectedTheme = (int)Theme.Default;
            }
            
        }

        public bool Islight { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDark { get; set; }
        public Framework.Std.Lists.ObservableRangeCollection<string> ThemeOptions { get; } = new Framework.Std.Lists.ObservableRangeCollection<string>();
        public ICommand ManageHostsCmd { get; }

        public int SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    if (ThemeOptions.Count == 3)
                        ThemeHelper.CurrentTheme = (Theme) value;
                    else
                        ThemeHelper.CurrentTheme = (Theme) (value + 1);

                    ThemeHelper.ChangeTheme(ThemeHelper.CurrentTheme, true);

                    switch (ThemeHelper.CurrentTheme)
                    {
                        case Theme.Light:
                            Islight = true;
                            IsDark = false;
                            IsDefault = false;
                            break;
                        case Theme.Dark:
                            IsDark = true;
                            Islight = false;
                            IsDefault = false;
                            break;
                        default:
                            IsDefault = true;
                            IsDark = false;
                            Islight = false;
                            break;
                    }
                }
            }
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
            set => SetProperty(ref _useFingerprint,
                value /*, null,  (currentValue, newValue) =>  ProveYouHaveFingers(currentValue, newValue)*/);

            //set => SetProperty(ref _useFingerprint, value, null,  (currentValue, newValue) => ProveYouHaveFingers());
            //set => SetProperty(ref _useFingerprint, value, async () => await ProveYouHaveFingers(), null);
        }

        public bool CanUseFingerprint
        {
            get => _canUseFingerprint;
            private set => SetProperty(ref _canUseFingerprint, value);
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
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
            _useFingerprint = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            await RefreshData();
            await base.OnInternalAppearingAsync();
        }

        protected override async Task OnInternalDisappearingAsync()
        {
            var tmp = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            if (tmp != _useFingerprint)
                if (await ProveYouHaveFingers())
                    Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);
            await base.OnInternalDisappearingAsync();
        }


        protected Task RefreshData(bool b = true)
        {
            //Xamarin.Essentials.Preferences
            UseFingerprint = Preferences.Get(PreferencesKeys.UseFingerprintKey, default(bool));
            EndReward = _rewardService.EndReward;
            IsRewarded = _rewardService.IsRewarded;
            return Task.FromResult(0);
        }

        //private async Task OnSwitchChanging()
        //{
        //    try
        //    {
        //        await UseFingerprintAction(!_useFingerprint);
        //    }
        //    catch (Exception ex)
        //    {
        //        var toto = ex.Message;
        //    }
        //}
        //private async Task UseFingerprintAction(bool value)
        //{
        //    if (value)
        //    {
        //        var result = await ProveYouHaveFingers();
        //        if (result)
        //        {
        //            _useFingerprint = value;
        //            Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);

        //        }
        //        else
        //            _useFingerprint = false;
        //        OnPropertyChanged(nameof(UseFingerprint));
        //    }
        //}
        private async Task<bool> ProveYouHaveFingers(bool retry = true)
        {
            //CrossFingerprint.Current.

            var task = await CrossFingerprint.Current.AuthenticateAsync(
                    new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!"))
                ;


            if (task.Authenticated)
                Preferences.Set(PreferencesKeys.UseFingerprintKey, UseFingerprint);
            else if (retry) return await ProveYouHaveFingers(false);

            return task.Authenticated;
        }


        private bool ProveYouHaveFingers(bool currentValue, bool newValue)
        {
            var result = false;

            var oo = new AuthenticationRequestConfiguration("Locked", "Prove you have fingers!");

            var t = CrossFingerprint.Current.AuthenticateAsync(
                oo);
            //t.Start();

            var toto = t.Wait(TimeSpan.FromSeconds(5));

            var t2 = t.ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                    result = t.Result.Authenticated;

                if (t.IsFaulted || t.IsCanceled)
                    result = false;
                if (!t.IsCompleted) result = false;
                result = t.Result.Authenticated;
            });
            //var statut = t.Status;


            Task.WaitAll(t2);
            //do
            //{
            //} while (!t.IsFaulted || !t.IsCompleted || 
            //         !t.IsCanceled || !t.IsCompletedSuccessfully);


            //if (t.Result)
            //{
            //    Preferences.Set(PreferencesKeys.UseFingerprintKey, newValue);
            //}
            //return t;
            return result;
        }
    }
}