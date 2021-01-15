﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Comtrols;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.About;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.NAS.SharedFolders;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSystemLogs;
using Doods.Xam.MonitorMyServer.Views.Settings;
using Doods.Xam.MonitorMyServer.Views.Synology.SynoStorage;
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


        /// <summary>
        /// 
        /// </summary>
        public MyCustomShellApp()
        {
            MessagingCenter.Subscribe<ConnctionService, Host>(
                this, MessengerKeys.HostChanged,
                async (sender, arg) => { MainThread.BeginInvokeOnMainThread(() => { InitList(arg); }); });

            
            var config =
                App.Container.Resolve<IConfiguration>();

            if (!config.ModeOmvOnlyKey)
            {
                FlyoutHeader = new FlyoutHeader("MMS_graphic.png");
            }
            else
            {
                FlyoutHeader = new FlyoutHeader("OMV_graphic.png");
            }


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
                                Title = Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
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
            // Item => section => content

            //this.PropertyChanged += OnPropertyChanged;
            //this.CurrentItem.PropertyChanged += CurrentItemOnPropertyChanged;
        }

        //private void CurrentItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    var name = e.PropertyName;
        //}

        //private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //Application.Current?.MainPage as Shell;

        //    //var current = Application.Current?.MainPage as Shell;
        //    //var current2 = Application.Current?.MainPage as IShellController;
        //    //var current3 = Application.Current?.MainPage as IPropertyPropagationController;
            
            
        //    // Shell Current => Application.Current?.MainPage as Shell;

        //    if (e.PropertyName == nameof(CurrentItem))
        //    {
        //        if (CurrentItem != null)
        //        {
        //            if (CurrentItem.Title == openmediavault.PerformanceStatistics || Current.Title == "OMV")
        //            {
        //                Shell.SetTabBarIsVisible(CurrentItem, true);
        //                Shell.SetTabBarIsVisible(CurrentItem?.CurrentItem, true);
        //                Shell.SetTabBarIsVisible(CurrentItem?.CurrentItem?.CurrentItem, true);
        //            }
        //        }
        //    }
        //}

       

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{

            
        //    base.OnNavigating(args);
        //}

        //protected override void OnNavigated(ShellNavigatedEventArgs args)
        //{
        //    base.OnNavigated(args);
        //}

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    //if(CurrentItem != null)
        //    //    if (args.Target.Location.OriginalString == "//OMV" || args.Target.Location.OriginalString == "//"+openmediavault.PerformanceStatistics)
        //    //    {

        //    //        Shell.SetTabBarIsVisible(CurrentItem, true);
        //    //        Shell.SetTabBarIsVisible(CurrentItem?.CurrentItem, true);
        //    //        //Shell.SetTabBarIsVisible(CurrentItem?.CurrentItem?.CurrentItem, true);
        //    //    }
        //    //    else
        //    //    {
        //    //        Shell.SetTabBarIsVisible(CurrentItem?.CurrentItem, false);

        //    //    }
        //    base.OnNavigating(args);
        //}


        private IEnumerable<FlyoutItem> GetNasPages()
        {
            yield return CreateFlyoutItem(openmediavault.SharedFolders,
                typeof(SharedFoldersPage));
        }

        private IEnumerable<FlyoutItem> GetSynoPages()
        {
            yield return CreateFlyoutItem("Syno",
                typeof(SynologyInfoPage));
            yield return CreateFlyoutItem(openmediavault.FileSystems,
                typeof(SynologyStoragePage));
        }

        private IEnumerable<FlyoutItem> GetLinuxPages()
        {
            yield return CreateFlyoutItem(openmediavault.FileSystems,
                typeof(Views.Linux.DisksUsage.DisksUsagePage));
        }
        private IEnumerable<FlyoutItem> GetOmvPages()
        {
            yield return new FlyoutItem
            {
                Title = "OMV",
                Items =
                {
                    CreateTabItem("OMV", typeof(OpenmediavaultDashboardPage)),
                    CreateTabItem(openmediavault.PerformanceStatistics,
                        typeof(OpenmediavaultStatisticsPage))
                },
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
                                    Title = Openmediavault.Mobile.Std.Resources.openmediavault.Homepage,
                                    ContentTemplate = new DataTemplate(typeof(MainPage))
                                }
                            }
                        }
                    }
                };

                Items.AddRange(homeItem);
            }

            if (!host.IsOmvServer && !host.IsSynoServer && host.IsSsh)
            {
                Items.AddRange(GetLinuxPages());
                
            }
            if (host.IsOmvServer) Items.AddRange(GetOmvPages());
            if (host.IsSynoServer) Items.AddRange(GetSynoPages());
            if (host.IsOmvServer) Items.AddRange(GetNasPages());
            if (host.IsSynoServer) Items.AddRange(GetNasPages());

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
                        Route = title,
                        Title = title,
                        ContentTemplate = new DataTemplate(page)
                    }
                }
            };
        }
    }
}