using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Models
{
    //public enum StateLoadingPage
    //{
    //    Before,
    //    Loading,
    //    Okay,
    //    Fault,
    //    Warning
    //}

    public interface IStateItem
    {
        bool IsRunning { get; }
    }

    public class ViewModelStateItem : BaseItem, IStateItem
    {
        private Color? _color;
        private SvgIconTarget _icon;
        private bool _isRunning;
        private ICommand _showCurrentCmd;

        public ViewModelStateItem(IViewModel viewModel)
        {
            ViewModel = viewModel;
            ShowCurrentCmd = ViewModel.CmdState;
            Icon = SvgIconTarget.Info;
        }

        public ICommand ShowCurrentCmd
        {
            get => _showCurrentCmd;
            set => SetProperty(ref _showCurrentCmd, value);
        }

        public IViewModel ViewModel { get; }

        public SvgIconTarget Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public Color? Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public async Task RunActionAsync(Func<Task> myAction, Action before, Action after)
        {
            before?.Invoke();
            MainThread.BeginInvokeOnMainThread(() => { IsRunning = true; });
            await myAction.Invoke();
            MainThread.BeginInvokeOnMainThread(() => { IsRunning = false; });
            after?.Invoke();
        }

        public void RunActions(Action myAction, Action before, Action after)
        {
            before?.Invoke();
            RunAction(myAction);
            after?.Invoke();
        }

        public TResult RunFunc<TResult>(Func<TResult> myFunc, Action before, Action after)
        {
            before?.Invoke();
            var result = RunFunc(myFunc);
            after?.Invoke();
            return result;
        }

        public TResult RunFunc<TResult>(Func<TResult> myFunc)
        {
            IsRunning = true;
            var result = myFunc.Invoke();
            IsRunning = false;
            return result;
        }

        public void RunAction(Action myAction)
        {
            IsRunning = true;
            myAction.Invoke();
            IsRunning = false;
        }
    }
}