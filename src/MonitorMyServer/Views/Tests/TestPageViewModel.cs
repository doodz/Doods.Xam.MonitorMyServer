using System.Windows.Input;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Tests
{
    public class TestPageViewModel : ViewModelWhithState
    {
        private readonly IRewardService _rewardService;

        public TestPageViewModel(IRewardService rewardService, IConfiguration configuration)
        {
            ShowRewardCmd = new Command(ExecuteRewardCmd);
            ResetRewardCmd = new Command(ExecuteResetReward);
            _rewardService = rewardService;
            BannerId = configuration.AdsKey;
            RewardedVideoId = configuration.RewardedVideoKey;
        }

        public string BannerId { get; }
        public string RewardedVideoId { get; }
        public ICommand ShowRewardCmd { get; }
        public ICommand ResetRewardCmd { get; }

        public bool Isrewarded => _rewardService.IsRewarded;

        private void ExecuteResetReward()
        {
            _rewardService.ResetRewardDate();
            OnPropertyChanged(nameof(Isrewarded));
        }

        private void ExecuteRewardCmd()
        {
            object obj = null;
            obj.ToString();//TODO the error;
            _rewardService.ShowRewardedVideo(() => OnPropertyChanged(nameof(Isrewarded)));
        }
    }
}