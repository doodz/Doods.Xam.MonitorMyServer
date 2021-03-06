﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Processes2
{
    public class ProcessesPageViewModel : ViewModelWhithState
    {
        private readonly ISshService _sshService;

        private IEnumerable<Process> _processes;
        private int _processesCount;

        public ProcessesPageViewModel(ISshService sshService)
        {
            Title = Resource.Processes;
            _sshService = sshService;
            StopProcessCommand = new Command(async o => await StopProcess(o));
        }

        public ICommand StopProcessCommand { get; }

        public IEnumerable<Process> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        public int ProcessesCount
        {
            get => _processesCount;
            set => SetProperty(ref _processesCount, value);
        }


        private async Task StopProcess(object obj)
        {
            if (obj == null) return;

            if (obj is Process p) await _sshService.RunCommand($"kill {p.Pid}");
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            SetLabelsStateItem(Resource.PleaseWait, Resource.PacketReading);
            ViewModelStateItem.IsRunning = true;
            //await _sshService.UpdateAptListRequest().ConfigureAwait(false);
            await GetProcesses();
            ViewModelStateItem.IsRunning = false;
            SetLabelsStateItem(Resource.PleaseWait,
                string.Format(Resource.ThereAre_0_Processes, ProcessesCount));
        }

        private async Task GetProcesses()
        {
            Processes = await _sshService.GetProcesses();
            ProcessesCount = _processes.Count();
        }
    }
}