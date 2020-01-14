using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;
using System.Threading.Tasks;

namespace Doods.Xam.MonitorMyServer.Views.Base
{
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
            _navigationService ?? (_navigationService = App.Container.ResolveKeyed<INavigationService>(App.NavigationServiceType));

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