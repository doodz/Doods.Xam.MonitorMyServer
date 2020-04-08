using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Extensions;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Comtrols;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views;
using Doods.Xam.MonitorMyServer.Views.About;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSystemLogs;
using Doods.Xam.MonitorMyServer.Views.Settings;
using Doods.Xam.MonitorMyServer.Views.SynologyInfo;
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

       

        public MyCustomShellApp()
        {
            MessagingCenter.Subscribe<ConnctionService, Host>(
                this, MessengerKeys.HostChanged,
                async (sender, arg) => { MainThread.BeginInvokeOnMainThread(() => { InitList(arg); }); });

            FlyoutHeader = new FlyoutHeader();
            var homeItem = new FlyoutItem
            {
                Title = Title = Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
                Items =
                {
                    new ShellSection
                    {
                        Items =
                        {
                            new ShellContent
                            {
                                Title =   Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
                                ContentTemplate = new DataTemplate(typeof(MainPage))
                            }
                        }
                    }
                }
            };
          
            Items.AddRange(homeItem);
          


            //var commandsItem = CreateFlyoutItem(openmediavault.ExecuteCommand,
            //    typeof(CustomCommandListPage));
            var settingsItem = CreateFlyoutItem(openmediavault.Settings,
                typeof(SettingsPage));
            var aboutItem = CreateFlyoutItem(openmediavault.About,
                typeof(AboutPage));


            //Items.AddRange(commandsItem, settingsItem, aboutItem);
            Items.AddRange(settingsItem, aboutItem);
            BindingContext = App.Container.Resolve<AppShellViewModel>();
        }

        

        private IEnumerable<FlyoutItem> GetSynoPages()
        {
            yield return CreateFlyoutItem("Syno",
                typeof(SynologyInfoPage));
        }


        private IEnumerable<FlyoutItem> GetOmvPages()
        {
           yield return  new FlyoutItem
            {
                Title = "OMV",
                Items =
                {
                    CreateTabItem("OMV", typeof(OpenmediavaultDashboardPage)),
                    CreateTabItem(openmediavault.PerformanceStatistics,
                        typeof(OpenmediavaultStatisticsPage))
                }
            };


           yield return CreateFlyoutItem(openmediavault.FileSystems,
                typeof(OpenmediavaultFileSystemsPage));
           yield return CreateFlyoutItem(openmediavault.Plugins,
                typeof(OpenmediavaultPluginsPage));
           yield return CreateFlyoutItem(openmediavault.SystemLogs,
               typeof(OpenmediavaultSystemLogsPage));

        }

        private void InitList(Host host)
        {
            
            Items.Clear();
            if (host.IsSsh && !host.IsOmvServer)
            {
                var homeItem = new FlyoutItem
                {
                    Title = Title = Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
                    Items =
                    {
                        new ShellSection
                        {
                            Items =
                            {
                                new ShellContent
                                {
                                    Title =   Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
                                    ContentTemplate = new DataTemplate(typeof(MainPage))
                                }
                            }
                        }
                    }
                };

                Items.AddRange(homeItem);
            }
             
            if (host.IsOmvServer) Items.AddRange(GetOmvPages());

            if(host.IsSynoServer) Items.AddRange(GetSynoPages());

            if (host.IsSsh)
            {
                var commandsItem = CreateFlyoutItem(openmediavault.ExecuteCommand,
                    typeof(CustomCommandListPage));
                Items.Add(commandsItem);
            }

            var settingsItem = CreateFlyoutItem(openmediavault.Settings,
                typeof(SettingsPage));
            var aboutItem = CreateFlyoutItem(openmediavault.About,
                typeof(AboutPage));


            Items.AddRange(settingsItem, aboutItem);
            CurrentItem = Items.First();
        }


        private static FlyoutItem CreateFlyoutItem(string title, Type page)
        {
            return new FlyoutItem
            {
                Title = title,
                Items =
                {
                    CreateTabItem(title, page)
                }
            };
        }

        private static Tab CreateTabItem(string title, Type page)
        {
            return new Tab
            {
                Title = title,
                Items =
                {
                    new ShellContent
                    {
                        Title = title,
                        ContentTemplate = new DataTemplate(page)
                    }
                }
            };
        }
    }
}