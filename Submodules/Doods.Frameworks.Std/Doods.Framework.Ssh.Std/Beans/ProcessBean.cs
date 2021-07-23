using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class ProcessBean : NotifyPropertyChangedBase
    {
        private string _command;
        private string _cpuTime;
        private int _pId;
        private string _tty;

        public ProcessBean(int pId, string tty, string cpuTime, string commandName, string command)
        {
            CommandName = commandName;
            _pId = pId;
            _tty = tty;
            _cpuTime = cpuTime;
            _command = command;
        }

        public string CommandName { get; }

        public int Pid
        {
            get => _pId;
            internal set => SetProperty(ref _pId, value);
        }

        public string Tty
        {
            get => _tty;
            internal set => SetProperty(ref _tty, value);
        }

        public string CpuTime
        {
            get => _cpuTime;
            internal set => SetProperty(ref _cpuTime, value);
        }

        public string Command
        {
            get => _command;
            internal set => SetProperty(ref _command, value);
        }
    }
}