﻿using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Ssh.Std;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.CustomCommandList
{
    public class CustomCommandListPageViewModel : DataTableItemsViewModel<CustomCommandSsh>
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly ISshService _sshService;
        private  ShellClient _shellClient;
        public ShellBox _shellBox;
        public ShellBox ShellBox
        {
            get { return _shellBox;} private set{ SetProperty(ref _shellBox, value); } }
        public CustomCommandListPageViewModel(ISshService sshService, IMessageBoxService messageBoxService)
        {
            _sshService = sshService;
           

            _messageBoxService = messageBoxService;
            RunCommand = new Command(Run);
        }

        public ICommand RunCommand { get; }

        protected override void AddItem(object obj)
        {
            NavigationService.NavigateAsync(nameof(AddCustomCommandPage));
        }

        private async void Run(object obj)
        {
            if (obj == null) return;
            if (obj is CustomCommandSsh item)
            {
                //var resutl = await _sshService.RunCommand(item.CommandString);
                //_messageBoxService.ShowAlert("Alert", resutl);
                toto(item.CommandString);
               
            }


        }

        protected override Task OnInternalAppearingAsync()
        {
            if (!_sshService.IsConnected())
            {
                _sshService.Connect();
            }
            _shellClient = _sshService.CreateShell();
            _shellClient.Connect();
            ShellBox = new ShellBox(_shellClient);
            ////_shellClient.Connect();

            return base.OnInternalAppearingAsync();
        }

        protected override Task OnInternalDisappearingAsync()
        {
            ShellBox.Unsubscribe();
            _shellClient.Stop();

            return  base.OnInternalDisappearingAsync();
        }

        private void toto(string totostr)
        {
            ShellBox.Execute(totostr);
        }

      


        protected override void EditItem(object obj)
        {
            if (obj == null) return;
            if (obj is CustomCommandSsh item)
                NavigationService.NavigateAsync(nameof(AddCustomCommandPage), new CustomCommandSshWrapper(item));
        }
    }
}