﻿using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Login;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostManagerPageViewModel : ViewModel
    {
        public ICommand AddHostCmd { get; }
       
        public ObservableRangeCollection<Host> Items { get; }

        private Host _selectedHost;

        public Host SelectedHost
        {
            get => _selectedHost;
            set => SetProperty(ref _selectedHost, value);
        }

        public HostManagerPageViewModel()
        {
            Title = Resource.Hosts;
            Items = new ObservableRangeCollection<Host>();

            AddHostCmd = new Command(() => NavigationService.NavigateAsync(nameof(LogInPage)));
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {

            await RefreshData();
        }


        private async Task RefreshData()
        {
            var lstHosts = await DataProvider.GetHostsAsync();        
            Items.ReplaceRange(lstHosts);
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            if (!Items.Any())
                Title = "No hosts detected :(.";
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {

            //var image2 = new SvgCachedImage()
            //{
            //    Source = SvgIconTarget.AddBox.ResourceFile,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = 100,
            //    DownsampleToViewSize = true,
            //    Aspect = Aspect.AspectFill,
            //    TransformPlaceholders = false,
            //    LoadingPlaceholder = "loading.png",
            //    ErrorPlaceholder = "error.png",
            //};
            yield return  new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Add",
                IsPrimary = true,
                Icon = "error.png"

            };
        }
    }
}
