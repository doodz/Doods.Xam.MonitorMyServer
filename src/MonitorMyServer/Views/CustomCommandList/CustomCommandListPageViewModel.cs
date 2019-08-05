using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.CustomCommandList
{

    public class CustomCommandListPageViewModel: DataTableItemsViewModel<CustomCommandSsh>
    {
        protected override void AddItem(object obj)
        {
            NavigationService.NavigateAsync(nameof(AddCustomCommandPage));
        }

        protected override void EditItem(object obj)
        {
            if (obj == null) return;
            if (obj is CustomCommandSsh item) NavigationService.NavigateAsync(nameof(AddCustomCommandPage), new CustomCommandSshWrapper(item));
        }

        
    }
}
