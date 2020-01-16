using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Extensions;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views;
using Doods.Xam.MonitorMyServer.Views.About;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics;
using Doods.Xam.MonitorMyServer.Views.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer
{
    public class MyCustomShellApp : Shell
    {
        //protected override void OnDisappearing()
        //{
        //    MessagingCenter.Unsubscribe<MainPageViewModel, Host>(this, MessengerKeys.HostChanged);
        //    base.OnDisappearing();
        //}

        //protected override void OnAppearing()
        //{
        //    MessagingCenter.Subscribe<MainPageViewModel, Host>(
        //        this, MessengerKeys.HostChanged, async (sender, arg) =>
        //        {
        //            MainThread.BeginInvokeOnMainThread(() => { InitList(arg); });

        //        });
        //    base.OnAppearing();
        //}

        private IEnumerable<FlyoutItem> OmvPages;
        private void InitList(Host host)
        {

            var home = Items.First();
            Items.Clear();
            Items.Add(home);
           

          

            if (host.IsOmvServer)
            {
               
                    Items.AddRange(OmvPages);
                
               
            }

            var commandsItem = CreateFlyoutItem("Commands", typeof(CustomCommandListPage));
            var settingsItem = CreateFlyoutItem("Settings", typeof(SettingsPage));
            var aboutItem = CreateFlyoutItem("About", typeof(AboutPage));


            Items.AddRange(commandsItem, settingsItem, aboutItem);
            CurrentItem = home;
        }

        public MyCustomShellApp()
        {

            MessagingCenter.Subscribe<MainPageViewModel, Host>(
                this, MessengerKeys.HostChanged, async (sender, arg) =>
                {
                    MainThread.BeginInvokeOnMainThread(() => { InitList(arg); });

                });

            FlyoutHeader = new Comtrols.FlyoutHeader();
            var homeItem = new FlyoutItem()
            {
                Title = Title = "Home",
                Items =
                {
                    new ShellSection()
                    {
                        Items =
                        {
                            new ShellContent()
                            {
                                Title = "Home",
                                ContentTemplate = new DataTemplate(typeof(MainPage))
                            }
                        }
                    }
                }
            };

            Items.AddRange(homeItem);
            var omvHomeItem = new FlyoutItem()
            {
                Title = "OMV",
                Items =
                {
                    CreateTabItem("OMV",typeof(OpenmediavaultDashboardPage)),
                    CreateTabItem("Statistics",typeof(OpenmediavaultStatisticsPage))
                }
            };
            var fileSystemItem = CreateFlyoutItem("File Systems", typeof(OpenmediavaultFileSystemsPage));
            var pluginItem = CreateFlyoutItem("Plugins", typeof(OpenmediavaultPluginsPage));

            OmvPages =new List<FlyoutItem>(){ omvHomeItem, fileSystemItem, pluginItem };

            Items.AddRange(OmvPages);


            var commandsItem = CreateFlyoutItem("Commands", typeof(CustomCommandListPage));
            var settingsItem = CreateFlyoutItem("Settings", typeof(SettingsPage));
            var aboutItem = CreateFlyoutItem("About", typeof(AboutPage));
           

            Items.AddRange(commandsItem,settingsItem, aboutItem);
            BindingContext = App.Container.Resolve<AppShellViewModel>();

            
        }


        private static FlyoutItem CreateFlyoutItem(string title, Type page)
        {
            return new FlyoutItem()
            {
                Title  = title,
                Items =
                {
                    CreateTabItem(title,page)
                }
            };
        }

        private static Tab CreateTabItem(string title, Type page)
        {
            return new Tab()
            {
                Title = title,
                Items =
                {
                    new ShellContent()
                    {
                        Title = title,
                        ContentTemplate = new DataTemplate(page)
                    }
                }
            };
        }

    }
}