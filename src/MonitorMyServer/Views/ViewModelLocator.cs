using System;
using System.Globalization;
using System.Reflection;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Login;
using TinyIoC;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views
{
    public static class ViewModelLocator
    {
        private static readonly TinyIoCContainer Container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);

        static ViewModelLocator()
        {
            Container = new TinyIoCContainer();
            Container.Register<MainPageViewModel>();
            Container.Register<LoginPageViewModel>();
            Container.Register<HostManagerPageViewModel>();
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            Container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view)) return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName =
                string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null) return;

            var viewModel = Container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}