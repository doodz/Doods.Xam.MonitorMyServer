using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Tests
{
    public class TestPageViewModel : ViewModelWhithState
    {
        public string BannerId { get; }
        public string RewardedVideoId { get; }
        public ICommand ShowRewardCmd { get; }
        public ICommand ResetRewardCmd { get; }
        private readonly IRewardService _rewardService;
        public TestPageViewModel(IRewardService rewardService, IConfiguration configuration)
        {
            ShowRewardCmd = new Command(ExecuteRewardCmd);
            ResetRewardCmd = new Command(ExecuteResetReward);
            _rewardService = rewardService;
            BannerId = configuration.AdsKey;
            RewardedVideoId = configuration.RewardedVideoKey;
        }

        public bool Isrewarded => _rewardService.IsRewarded;

        private void ExecuteResetReward()
        {
            _rewardService.ResetRewardDate();
            this.OnPropertyChanged(nameof(Isrewarded));

        }

        private void ExecuteRewardCmd()
        {
            _rewardService.ShowRewardedVideo(() => this.OnPropertyChanged(nameof(Isrewarded)));
        }


    }
}
