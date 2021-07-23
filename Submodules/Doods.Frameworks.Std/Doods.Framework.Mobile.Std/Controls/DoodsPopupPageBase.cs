using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Doods.Framework.Mobile.Std.controls
{
    public class DoodsPopupPageBase : PopupPage
    {
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        protected async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}