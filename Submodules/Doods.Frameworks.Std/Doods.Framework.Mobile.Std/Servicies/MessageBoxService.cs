using System;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Resources;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Servicies
{
    public class MessageBoxService : IMessageBoxService
    {
        private static Page CurrentMainPage => Application.Current.MainPage;

        public async void ShowAlert(string title, string message, Action onClosed = null)
        {
            await CurrentMainPage.DisplayAlert(title, message, Resource.ButtonOK);
            onClosed?.Invoke();
        }


        public async Task<bool> ShowAction(string title, string message, Action<bool> onClosed = null)
        {
            var result = await CurrentMainPage.DisplayAlert(title, message, Resource.ButtonOK, "nok");
            onClosed?.Invoke(result);
            return result;
        }

        public async Task<string> ShowActionSheet(string title, string cancel, string destruction = null,
            string[] buttons = null)
        {
            var displayButtons = buttons ?? new string[] { };
            var action = await CurrentMainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
            return action;
        }
    }
}