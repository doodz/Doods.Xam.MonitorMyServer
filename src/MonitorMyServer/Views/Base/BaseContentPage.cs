using System;
using System.Linq;
using Autofac;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Std.Extensions;
using Doods.Xam.MonitorMyServer;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Mvvm
{
    public class BaseContentPage<T> : BaseContentPage where T : ViewModel
    {
        protected override ViewModel InitializeViewModelInternal()
        {
            return App.Container.Resolve<T>();
        }
    }

    public class ContentPageWithStateTemplate : ContentView
    {
        private readonly NotificationView _notificationView;

        public ContentPageWithStateTemplate()
        {
            _notificationView = new NotificationView();
            Content = new StackLayout
            {
                Children =
                {
                    _notificationView,
                    new ScrollView
                    {
                        Content = new ContentPresenter()
                    }
                }
            };


            BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (sender is IViewModelWhithState mv) _notificationView.BindingContext = mv.ViewModelStateItem;
        }
    }


    public class BaseContentPageWithState : BaseContentPage
    {
        private readonly ControlTemplate _contentPageWithStateTemplate =
            new ControlTemplate(typeof(ContentPageWithStateTemplate));

        public BaseContentPageWithState()
        {
            ControlTemplate = _contentPageWithStateTemplate;
        }
    }


    public class BaseContentPage : ContentPage
    {
        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel),
            typeof(ViewModel), typeof(BaseContentPage));

        public static readonly BindableProperty PageProperty =
            BindableProperty.Create(nameof(Page),
                typeof(RootPages), typeof(BaseContentPage), RootPages.None);

        private bool _isAppearing;

        private bool _isLoaded;
        private bool _rebind;

        protected ListView ListView { get; set; }

        public ViewModel ViewModel
        {
            get => (ViewModel) GetValue(ViewModelProperty);
            set
            {
                if (value == null) return;
                SetValue(ViewModelProperty, value);
                OnViewModelChanged();
            }
        }

        public RootPages Page
        {
            get => (RootPages) GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        protected void Start(ViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected void Start(bool rebind)
        {
            _rebind = rebind;
        }

        protected void OnViewModelChanged()
        {
            if (!_rebind)
                BindingContext = ViewModel;

            ViewModel.SetCurrentPage(Page);

            ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItemsCount")
                    if (ViewModel.CanUpdateToolBar())
                        UpdateToolBar();

                if (e.PropertyName == "UpdateToolBar")
                    UpdateToolBar();

                if (e.PropertyName == "Item")
                    UpdateToolBar();
            };

            UpdateToolBar();
        }

        protected void UpdateToolBar()
        {
            try
            {
                while (ToolbarItems.IsNotEmpty())
                    ToolbarItems.RemoveAt(0);

                var toolBarItems = ViewModel.GetToolBarItemDescriptions();
                if (toolBarItems.IsNotEmpty())
                    ToolbarItems.AddRange(toolBarItems.Select(t =>
                    {
                        var item = new ToolbarItem
                        {
                            Command = t.Command,
                            Text = t.Text,
                            Icon = t.Icon,
                            Order = t.IsPrimary ? ToolbarItemOrder.Primary : ToolbarItemOrder.Secondary
                        };

                        if (!string.IsNullOrEmpty(t.CommandParameterName) && t.CommandParameterSourceForBinding != null)
                            item.SetBinding(MenuItem.CommandParameterProperty, new Binding
                            {
                                Path = t.CommandParameterName,
                                Source = t.CommandParameterSourceForBinding
                            });


                        return item;
                    }));
            }
            catch (Exception e)
            {
                //TODO:Logg
                var msg = e.Message;
            }
        }

        public void SendAppearing(bool force = false)
        {
            if (force)
                _isAppearing = false;

            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            if (_isAppearing)
                return;

            _isAppearing = true;

            base.OnAppearing();
            if (!_isLoaded)
            {
                _isLoaded = true;
                if (_rebind)
                    ViewModel = BindingContext as ViewModel;
            }

            if (ViewModel == null)
                ViewModel = InitializeViewModelInternal();

            ViewModel.SetCurrentPage(Page);
            await ViewModel.OnAppearingAsync();
        }

        protected virtual ViewModel InitializeViewModelInternal()
        {
            return null;
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await ViewModel.OnDisappearingAsync();
            _isAppearing = false;
        }


        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }

        protected Style GetStyle(string name)
        {
            var style = Application.Current.Resources[name] as Style;
            return style;
        }

        public void SetContext(object context)
        {
            SetContextInternal(context);
        }

        protected virtual void SetContextInternal(object context)
        {
        }
    }
}