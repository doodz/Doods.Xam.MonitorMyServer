using System;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Data;
using MarcTron.Plugin;
using MarcTron.Plugin.CustomEventArgs;
using Xamarin.Essentials;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IRewardService
    {
        bool IsRewarded { get; }
        DateTime EndReward { get; }
        event EventHandler OnRewarded;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdLeftApplication;
        void ShowRewardedVideo();
        void ShowRewardedVideo(Action myAction);
        void SetCurrentAction(Action myAction);
        void ResetRewardDate();
    }

    internal class RewardService : IRewardService
    {
        private readonly string _rewardedVideoKey;
        private Action _currentAction;
        private bool _isRewarded;
        private ILogger _logger;
        public RewardService(IConfiguration configuration,ILogger logger)
        {
            _logger = logger;
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

        public bool IsRewarded => CheckIfIsRewarded();

        public DateTime EndReward { get; private set; }

        public void ResetRewardDate()
        {
            EndReward = DateTime.Today.AddDays(-12);
            Preferences.Set(PreferencesKeys.LastRewardForUpdateKey, EndReward);

            _isRewarded = false;
        }

        public void ShowRewardedVideo()
        {
            if (CrossMTAdmob.Current.IsRewardedVideoLoaded())
                CrossMTAdmob.Current.ShowRewardedVideo();
            else
                CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
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

        private bool CheckIfIsRewarded()
        {
            EndReward = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
            _isRewarded = (EndReward - DateTime.Today).TotalDays >= 0;
            return _isRewarded;
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

        public bool IsRewardedVideoLoaded()
        {
            return CrossMTAdmob.Current.IsRewardedVideoLoaded();
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
            _logger.Info($"/!\\ OnRewardedVideoAdFailedToLoad : RewardAmount={ e.RewardAmount},ErrorCode={ e.ErrorCode},RewardType={ e.RewardType}");

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