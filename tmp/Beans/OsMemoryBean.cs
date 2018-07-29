using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class OsMemoryBean : NotifyPropertyChangedBase
    {
        private MemoryBean _totalMemory;
        private MemoryBean _totalUsed;
        private MemoryBean _totalFree;
        private float _percentageUsed;
        private string _errorMessage;

        public MemoryBean TotalMemory
        {
            get => _totalMemory;
            set => SetProperty(ref _totalMemory, value);
        }

        public MemoryBean TotalUsed
        {
            get => _totalUsed;
            set => SetProperty(ref _totalUsed, value);
        }

        public MemoryBean TotalFree
        {
            get => _totalFree;
            set => SetProperty(ref _totalFree, value);
        }

        public float PercentageUsed
        {
            get => _percentageUsed;
            set => SetProperty(ref _percentageUsed, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public OsMemoryBean(long totalMemory, long totalUsed)
        {
            TotalMemory = MemoryBean.From(Memory.KB, totalMemory);
            TotalUsed = MemoryBean.From(Memory.KB, totalUsed);
            TotalFree = MemoryBean.From(Memory.KB, totalMemory - totalUsed);
            PercentageUsed = (float) totalUsed / (float) totalMemory;
        }

        public OsMemoryBean(string str)
        {
            ErrorMessage = str;
        }
    }
}