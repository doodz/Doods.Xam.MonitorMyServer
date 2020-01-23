using System;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Data;
using MarcTron.Plugin;
using MarcTron.Plugin.CustomEventArgs;
using Xamarin.Essentials;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IRewardService
    {
        bool IsRewarded { get; }
        event EventHandler OnRewarded;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdLeftApplication;
        void ShowRewardedVideo();
        void ShowRewardedVideo(Action myAction);
        void SetCurrentAction(Action myAction);
        void ResetRewardDate();
        DateTime EndReward { get; }
    }

    internal class RewardService : IRewardService
    {
        private readonly string _rewardedVideoKey;
        private Action _currentAction;
        private bool _isRewarded;

        public RewardService(IConfiguration configuration)
        {
            CrossMTAdmob.Current.LoadRewardedVideo(configuration.RewardedVideoKey);
            _rewardedVideoKey = configuration.RewardedVideoKey;

            CrossMTAdmob.Current.OnRewarded += Current_OnRewarded;
            ; //When the user gets a reward
            CrossMTAdmob.Current.OnRewardedVideoAdClosed += Current_OnRewardedVideoAdClosed;
            ; //When the ads is closed
            CrossMTAdmob.Current.OnRewardedVideoAdFailedToLoad += Current_OnRewardedVideoAdFailedToLoad;
            ; //When the ads fails to load
            CrossMTAdmob.Current.OnRewardedVideoAdLeftApplication += Current_OnRewardedVideoAdLeftApplication;
            ; //When the users leaves the application
            //var rewardDate = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
            CheckIfIsRewarded();
        }

        public event EventHandler OnRewarded;
        public event EventHandler OnRewardedVideoAdClosed;
        public event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        public event EventHandler OnRewardedVideoAdLeftApplication;

        private bool CheckIfIsRewarded()
        {
            EndReward = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
            _isRewarded = (EndReward - DateTime.Today).TotalDays >=0;
            return _isRewarded;
        }

        public bool IsRewarded => CheckIfIsRewarded();
        
        public DateTime EndReward { get; private set; }

        public void ResetRewardDate()
        {
            EndReward = DateTime.Today.AddDays(-12);
            Preferences.Set(PreferencesKeys.LastRewardForUpdateKey, EndReward);
           
            _isRewarded = false;
        }

        private void AddRewardeValue()
        {
            if (IsRewarded)
            {
               
                var expi = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
                AddRewardeFromDate(expi);
            }
            else
            {
                AddRewardeFromDate(DateTime.Today);
            }
        }

        private void AddRewardeFromDate(DateTime date)
        {
            EndReward = date.AddDays(7);
            Preferences.Set(PreferencesKeys.LastRewardForUpdateKey, EndReward);
        }

        public void ShowRewardedVideo()
        {
           if (CrossMTAdmob.Current.IsRewardedVideoLoaded())
                CrossMTAdmob.Current.ShowRewardedVideo();
           else
           {
               CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);

           }
        }

        public bool IsRewardedVideoLoaded()
        {
            
            return CrossMTAdmob.Current.IsRewardedVideoLoaded();
        }

        public void ShowRewardedVideo(Action myAction)
        {
            SetCurrentAction(myAction);
            ShowRewardedVideo();
        }

        public void SetCurrentAction(Action myAction)
        {
            _currentAction = myAction;
        }

        private void Current_OnRewarded(object sender, MTEventArgs e)
        {
            _isRewarded = true;
            AddRewardeValue();
            CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewarded?.Invoke(this, e);
        }

        private void Current_OnRewardedVideoAdLeftApplication(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewardedVideoAdLeftApplication?.Invoke(this, e);
        }

        private void Current_OnRewardedVideoAdFailedToLoad(object sender, MTEventArgs e)
        {
            _currentAction?.Invoke();
            //CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewardedVideoAdFailedToLoad?.Invoke(this, e);
        }

        private void Current_OnRewardedVideoAdClosed(object sender, EventArgs e)
        {
            if (_isRewarded)
                _currentAction?.Invoke();

            CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewardedVideoAdClosed?.Invoke(this, e);
        }
    }
}