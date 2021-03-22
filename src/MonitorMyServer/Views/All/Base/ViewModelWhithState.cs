using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;

namespace Doods.Xam.MonitorMyServer.Views.Base
{
    public interface IViewModelWhithState
    {
        ViewModelStateItem ViewModelStateItem { get; }
        //void SetCommandForStateView(ICommand command);
        //void SetLabelsStateItem(string title, string description)
    }

    public class ViewModelWhithState : ViewModel, IViewModelWhithState
    {
        private ViewModelStateItem _viewModelStateItem;

        public ViewModelWhithState()
        {
            _viewModelStateItem = new ViewModelStateItem(this);
        }

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        protected void SetCommandForStateView(ICommand command)
        {
            ViewModelStateItem.ShowCurrentCmd = command;
        }

        protected void SetLabelsStateItem(string title, string description)
        {
            ViewModelStateItem.Title = title;
            ViewModelStateItem.Description = description;
        }


        protected override void OnFinishLoading(LoadingContext context)
        {
            _viewModelStateItem.IsRunning = false;
            base.OnFinishLoading(context);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            _viewModelStateItem.IsRunning = true;
            base.OnInitializeLoading(context);
        }

        protected override Task OnInternalDisappearingAsync()
        {
            _viewModelStateItem.IsRunning = true;
            return base.OnInternalDisappearingAsync();
        }
    }
}