using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Plugin.Fingerprint;

namespace Doods.Xam.MonitorMyServer.Views.Settings
{
    public class SettingsPAgeViewModel : ViewModel
    {
        private bool _useFingerprint;
        private bool _canUseFingerprint;

        public bool UseFingerprint
        {
            get => _useFingerprint;
            set => SetProperty(ref _useFingerprint, value,async () =>await  ProveYouHaveFingers(),null);
        }

        public bool CanUseFingerprint { get=>_canUseFingerprint;
            private set => SetProperty(ref _canUseFingerprint, value);
        }

        private IRewardService _rewardService;

        public SettingsPAgeViewModel(IRewardService rewardService)
        {
            _rewardService = rewardService;
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
           UseFingerprint = false;

           


            return Task.FromResult(0);
        }

        private async Task ProveYouHaveFingers(bool retry = true)
        {
            //CrossFingerprint.Current.
           
            var result = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!");
            if (result.Authenticated)
            {
                // do secret stuff :)
            }
            else
            {
                await ProveYouHaveFingers(false);
            }
            await Task.FromResult(0);
        }
    }

  
}
