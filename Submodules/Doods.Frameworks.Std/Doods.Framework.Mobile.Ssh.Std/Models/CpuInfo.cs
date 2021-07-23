using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class CpuInfo : NotifyPropertyChangedBase
    {
        private string _architecture;
        private string _byteOrder;
        private int _corePerSocket;
        private int _cpu;

        private double _cpuMaxMHz;
        private double _cpuMinMHz;
        private string _cpuOpMode;

        private string _modelName;
        private string _onlineCpuList;
        private int _socket;

        private int _threadPerCore;
        private string _vendorId;


        public int ThreadPerCore
        {
            get => _threadPerCore;
            set => SetProperty(ref _threadPerCore, value);
        }

        public int CorePerSocket
        {
            get => _corePerSocket;
            set => SetProperty(ref _corePerSocket, value);
        }

        public int Socket
        {
            get => _socket;
            set => SetProperty(ref _socket, value);
        }

        public int Cpu
        {
            get => _cpu;
            set => SetProperty(ref _cpu, value);
        }

        public double CpuMaxMHz
        {
            get => _cpuMaxMHz;
            set => SetProperty(ref _cpuMaxMHz, value);
        }

        public double CpuMinMHz
        {
            get => _cpuMinMHz;
            set => SetProperty(ref _cpuMinMHz, value);
        }


        public string ModelName
        {
            get => _modelName;
            set => SetProperty(ref _modelName, value);
        }


        public string Architecture
        {
            get => _architecture;
            set => SetProperty(ref _architecture, value);
        }


        public string ByteOrder
        {
            get => _byteOrder;
            set => SetProperty(ref _byteOrder, value);
        }

        public string OnlineCpuList
        {
            get => _onlineCpuList;
            set => SetProperty(ref _onlineCpuList, value);
        }

        public string CpuOpMode
        {
            get => _cpuOpMode;
            set => SetProperty(ref _cpuOpMode, value);
        }

        public string VendorId
        {
            get => _vendorId;
            set => SetProperty(ref _vendorId, value);
        }
    }
}