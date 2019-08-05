﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Base
{
    public interface IDataTableItemsViewModel<T> where T : TableBase
    {
    }

    public abstract class DataTableItemsViewModel<T> : ViewModel, IDataTableItemsViewModel<T> where T : TableBase, new()
    {
        private T _selectedItem;

        public DataTableItemsViewModel()
        {
            Items = new ObservableRangeCollection<T>();
            AddItemCommand = new Command(AddItem);
            DeleteItemCommand = new Command(DeleteItem);
            EditItemCommand = new Command(EditItem);
        }

        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ObservableRangeCollection<T> Items { get; }


        public T SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        protected abstract void AddItem(object obj);


        protected abstract void EditItem(object obj);


        private void DeleteItem(object obj)
        {
            if (obj == null) return;

            if (obj is T h)
            {
                var i = Items.IndexOf(h);
                if (i < 0) return;
                Items.RemoveAt(i);
                DataProvider.DeleteItemAsync(h);
            }
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            await RefreshData();
        }


        protected async Task RefreshData()
        {
            var items = await DataProvider.GetItemsAsync<T>();
            Items.ReplaceRange(items);
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {
            var image1 = SvgIconTarget.AddBox.ResourceFile;
            var image2 = new SvgCachedImage
            {
                Source = SvgIconTarget.AddBox.ResourceFile,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100,
                DownsampleToViewSize = true,
                Aspect = Aspect.AspectFill,
                TransformPlaceholders = false,
                LoadingPlaceholder = "loading.png",
                ErrorPlaceholder = "error.png"
            };


            var image3 = new FileImageSource();
            image3.File = image1;

            yield return new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Add",
                IsPrimary = true,
                Command = AddItemCommand,
                Icon = image3
            };
        }
    }
}