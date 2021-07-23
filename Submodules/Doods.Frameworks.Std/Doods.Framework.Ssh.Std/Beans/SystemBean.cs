using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class SystemBean : NotifyPropertyChangedBase
    {
        private double _averageLoad;

        private string _cpuSerial;
        private string _distributionName;

        private OsMemoryBean _osMemory;

        private double _uptime;

        public string DistributionName
        {
            get => _distributionName;
            set => SetProperty(ref _distributionName, value);
        }

        public OsMemoryBean OsMemory
        {
            get => _osMemory;
            set => SetProperty(ref _osMemory, value);
        }

        public string CpuSerial
        {
            get => _cpuSerial;
            set => SetProperty(ref _cpuSerial, value);
        }

        public double AverageLoad
        {
            get => _averageLoad;
            set => SetProperty(ref _averageLoad, value);
        }

        public double Uptime
        {
            get => _uptime;
            set => SetProperty(ref _uptime, value);
        }
    }
}