using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Data.Nas;
using Doods.Xam.MonitorMyServer.Resx.Webmin.package_updates;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Doods.Xam.MonitorMyServer.Views.NAS.PackageUpdates
{
    public class PackageUpdatesViewModel : ViewModelWhithState
    {
        private readonly IPackageUpdates _packageUpdates;
        private IList<Package> _packages;
        public ICommand SelectAllCmd { get; }
        public ICommand InvertSelectCmd { get; }

        public PackageUpdatesViewModel(IPackageUpdates packageUpdates)
        {
            _packageUpdates = packageUpdates;
            SelectAllCmd = new Command(_ => SelectAll());
            InvertSelectCmd = new Command(_ => InvertSelect());
        }

        public IList<Package> Packages
        {
            get => _packages;
            set => SetProperty(ref _packages, value);
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                
                   await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem(Webmin_package_updates.view_title, Webmin_package_updates.index_refresh),
                    () => { SetLabelsStateItem(Webmin_package_updates.view_title, openmediavault.Done___); });
            }
            catch (Exception e)
            {
                SetLabelsStateItem("Error", e.Message);
            }

            await base.OnInternalAppearingAsync();
        }


        private void SelectAll()
        {
            Packages?.ForEach(p=> p.IsSelected = true);
        }
        private void InvertSelect()
        {
            Packages?.ForEach(p => p.IsSelected = !p.IsSelected);
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetSharedFolders());
        }

        private async Task GetSharedFolders()
        {
            var result = await _packageUpdates.GetPackages();
            Packages = result?.ToList();
            //Packages = new List<Package>();
        }
    }
}