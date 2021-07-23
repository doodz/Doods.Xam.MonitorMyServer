// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.

using System;
using System.Threading.Tasks;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IMessageBoxService
    {
        Task<bool> ShowAction(string title, string message, Action<bool> onClosed = null);
        void ShowAlert(string title, string message, Action onClosed = null);
        Task<string> ShowActionSheet(string title, string cancel, string destruction, string[] buttons = null);
    }
}