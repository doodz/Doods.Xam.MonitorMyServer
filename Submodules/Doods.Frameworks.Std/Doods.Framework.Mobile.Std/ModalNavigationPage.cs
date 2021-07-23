using Autofac.Features.AttributeFilters;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Interfaces;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std
{
    public class ModalNavigationPage : NavigationPage
    {
        private readonly INavigationService _navigationService;

        public ModalNavigationPage([KeyFilter(NavigationServiceType.ViewNavigation)]
            INavigationService navigationService, Page root) : base(root)
        {
            _navigationService = navigationService;
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.GoBack();
            return base.OnBackButtonPressed();
        }
    }
}