using System.Collections.Generic;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class MemoryUsage : NotifyPropertyChangedBase
    {
        private string _errorMessage;
        private Dictionary<string, long> _memoryDataValues;
        private float _percentageUsed;
        private MemoryBean _totalFree;
        private MemoryBean _totalMemory;
        private MemoryBean _totalUsed;

        public Dictionary<string, long> MemoryDataValues
        {
            get => _memoryDataValues;
            set => SetProperty(ref _memoryDataValues, value);
        }

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
    }
}