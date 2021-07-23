using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class Process : NotifyPropertyChangedBase
    {
        private string _command;
        private string _commandName;
        private string _cpuTime;
        private int _pId;
        private string _tty;


        public string CommandName
        {
            get => _commandName;
            set => SetProperty(ref _commandName, value);
        }

        public int Pid
        {
            get => _pId;
            set => SetProperty(ref _pId, value);
        }

        public string Tty
        {
            get => _tty;
            set => SetProperty(ref _tty, value);
        }

        public string CpuTime
        {
            get => _cpuTime;
            set => SetProperty(ref _cpuTime, value);
        }

        public string Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }
    }
}