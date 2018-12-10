using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Models;

namespace Doods.Xam.MonitorMyServer.Views.Base
{
    public class ViewModelWhithState : ViewModel
    {
        private ViewModelStateItem _viewModelStateItem;

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        public ViewModelWhithState()
        {
            _viewModelStateItem = new ViewModelStateItem(this);
        }

        protected void SetCommandForStateView(ICommand command)
        {
            ViewModelStateItem.ShowCurrentCmd = command;
        }

        protected void SetLabelsStateItem(string title,string description)
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


    public class ViewModel : ViewModelBase
    {
        private RootPages _currentRootPage;
        private IDataProvider _dataProvider;
        private INavigationService _navigationService;

        protected ViewModel() : base(App.Container.Resolve<ILogger>(), App.Container.Resolve<ITelemetryService>())
        {
        }

        public override IColorPalette ColorPalette =>
            _colorPalette ?? (_colorPalette = App.Container.Resolve<IColorPalette>());

        protected IDataProvider DataProvider =>
            _dataProvider ?? (_dataProvider = App.Container.Resolve<IDataProvider>());

        public INavigationService NavigationService =>
            _navigationService ?? (_navigationService = App.Container.Resolve<INavigationService>());

        public RootPages CurrentRootPage
        {
            get => _currentRootPage;
            private set => SetProperty(ref _currentRootPage, value);
        }

        public void SetCurrentPage(RootPages page)
        {
            CurrentRootPage = page;
        }

        protected virtual Task InternalLoadAsync(LoadingContext context)
        {
            return Task.FromResult(0);
        }

        protected override Task LoadAsync(LoadingContext context)
        {
            return InternalLoadAsync(context);
        }
    }
}