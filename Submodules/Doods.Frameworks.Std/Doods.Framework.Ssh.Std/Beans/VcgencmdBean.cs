using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class VcgencmdBean : NotifyPropertyChangedBase
    {
        /// <summary>
        ///     ARM frequency in Hz.
        /// </summary>
        private long _armFrequency;

        /// <summary>
        ///     CORE frequency in Hz.
        /// </summary>
        private long _coreFrequency;

        /// <summary>
        ///     Volts of CORE.
        /// </summary>
        private double _coreVolts;

        /// <summary>
        ///     CPU temperature in Celsius.
        /// </summary>
        private double _cpuTemperature;

        /// <summary>
        ///     Version of vcgencmd.
        /// </summary>
        private string _version;

        public double CpuTemperature
        {
            get => _cpuTemperature;
            set => SetProperty(ref _cpuTemperature, value);
        }

        public long CoreFrequency
        {
            get => _coreFrequency;
            set => SetProperty(ref _coreFrequency, value);
        }

        public long ArmFrequency
        {
            get => _armFrequency;
            set => SetProperty(ref _armFrequency, value);
        }

        public double CoreVolts
        {
            get => _coreVolts;
            set => SetProperty(ref _coreVolts, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }
    }
}