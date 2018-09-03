using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;

namespace Doods.Xam.MonitorMyServer.Views.Base
{
    public class ViewModel : ViewModelBase
    {
        private RootPages _currentRootPage;
        private IDataProvider _dataProvider;

        protected ViewModel() : base(App.Container.Resolve<ILogger>(), App.Container.Resolve<ITelemetryService>())
        {
        }

        public IDataProvider DataProvider => _dataProvider ?? (_dataProvider = App.Container.Resolve<IDataProvider>());


        public RootPages CurrentRootPage
        {
            get => _currentRootPage;
            private set => SetProperty(ref _currentRootPage, value);
        }

        public void SetCurrentPage(RootPages page)
        {
            CurrentRootPage = page;
        }
    }
}