using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class ProcessBean : NotifyPropertyChangedBase
    {
        public string CommandName { get; }
        private string _command;
        private string _cpuTime;
        private int _pId;
        private string _tty;

        public ProcessBean(int pId, string tty, string cpuTime,string commandName, string command)
        {
            CommandName = commandName;
            _pId = pId;
            _tty = tty;
            _cpuTime = cpuTime;
            _command = command;
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