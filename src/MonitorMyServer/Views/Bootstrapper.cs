using Autofac;
using Doods.Xam.MonitorMyServer.Views.About;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Login;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics;
using Doods.Xam.MonitorMyServer.Views.Processes2;
using Doods.Xam.MonitorMyServer.Views.Tests;

namespace Doods.Xam.MonitorMyServer.Views
{
    internal class Bootstrapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterType<LoginPageViewModel>().AsSelf();
            builder.RegisterType<HostManagerPageViewModel>().AsSelf();
            builder.RegisterType<EnumerateAllServicesFromAllHostsViewModel>().AsSelf();
            builder.RegisterType<AptUpdatesPageViewModel>().AsSelf();
            builder.RegisterType<AppShellViewModel>().AsSelf();
            builder.RegisterType<AboutPageViewModel>().AsSelf();
            builder.RegisterType<AddCustomCommandPageViewModel>().AsSelf();
            builder.RegisterType<CustomCommandListPageViewModel>().AsSelf();
            builder.RegisterType<ProcessesPageViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultDashboardViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultStatisticsViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultSettingsViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultFileSystemsViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultAddFileSystemViewModel>().AsSelf();
            builder.RegisterType<OpenmediavaultPluginsViewModel>().AsSelf();
            builder.RegisterType<TestPageViewModel>().AsSelf();
        }
    }
}