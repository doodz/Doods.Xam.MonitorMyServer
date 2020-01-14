using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Data;
using MarcTron.Plugin;
using MarcTron.Plugin.CustomEventArgs;
using Xamarin.Essentials;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IRewardService
    {
        event EventHandler OnRewarded;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdLeftApplication;
        bool IsRewarded { get; }
        void ShowRewardedVideo();
        void ShowRewardedVideo(Action myAction);
        void SetCurrentAction(Action myAction);
        void ResetRewardDate();

    }
    internal class RewardService : IRewardService
    {

       public event EventHandler OnRewarded;
       public event EventHandler OnRewardedVideoAdClosed;
       public event EventHandler OnRewardedVideoAdFailedToLoad;
       public event EventHandler OnRewardedVideoAdLeftApplication;
        private readonly string _rewardedVideoKey;
        private bool _isRewarded;
        private Action _currentAction;
        public RewardService(IConfiguration configuration)
        {
            CrossMTAdmob.Current.LoadRewardedVideo(configuration.RewardedVideoKey);
            _rewardedVideoKey = configuration.RewardedVideoKey;
            
            CrossMTAdmob.Current.OnRewarded += Current_OnRewarded; ;//When the user gets a reward
            CrossMTAdmob.Current.OnRewardedVideoAdClosed += Current_OnRewardedVideoAdClosed; ;//When the ads is closed
            CrossMTAdmob.Current.OnRewardedVideoAdFailedToLoad += Current_OnRewardedVideoAdFailedToLoad; ;      //When the ads fails to load
            CrossMTAdmob.Current.OnRewardedVideoAdLeftApplication += Current_OnRewardedVideoAdLeftApplication; ; //When the users leaves the application
            var rewardDate = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
            _isRewarded = (rewardDate - DateTime.Today).TotalDays < 8;
        }

      

        public bool IsRewarded
        {
            get
            {
                var rewardDate = Preferences.Get(PreferencesKeys.LastRewardForUpdateKey, default(DateTime));
                _isRewarded = (DateTime.Today - rewardDate).TotalDays < 8;
                return _isRewarded;
            }
        }


        public void ResetRewardDate()
        {
            Preferences.Set(PreferencesKeys.LastRewardForUpdateKey, DateTime.Today.AddDays(-12));
            _isRewarded = false;
        }


        public void ShowRewardedVideo()
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
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
            Preferences.Set(PreferencesKeys.LastRewardForUpdateKey, DateTime.Today);
            OnRewarded?.Invoke(this,e);
        }

        private void Current_OnRewardedVideoAdLeftApplication(object sender, System.EventArgs e)
        {
            
            CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewardedVideoAdLeftApplication?.Invoke(this,e);
        }

        private void Current_OnRewardedVideoAdFailedToLoad(object sender, MTEventArgs e)
        {
            _isRewarded = true;
            _currentAction?.Invoke();
            OnRewardedVideoAdFailedToLoad?.Invoke(this,e);
        }

        private void Current_OnRewardedVideoAdClosed(object sender, System.EventArgs e)
        {
            if (_isRewarded)
                _currentAction?.Invoke();
           
            CrossMTAdmob.Current.LoadRewardedVideo(_rewardedVideoKey);
            OnRewardedVideoAdClosed?.Invoke(this,e);
        }
    }
}
