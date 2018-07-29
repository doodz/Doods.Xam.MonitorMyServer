
using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class DiskUsageBean : NotifyPropertyChangedBase
    {
        private string _fileSystem;
        private string _size;
        private string _used;
        private string _available;
        private string _usedPercent;
        private string _mountedOn;


        public string FileSystem
        {
            get => _fileSystem;
            set => SetProperty(ref _fileSystem, value);
        }

        public string Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public string Used
        {
            get => _used;
            set => SetProperty(ref _used, value);
        }

        public string Available
        {
            get => _available;
            set => SetProperty(ref _available, value);
        }

        public string UsedPercent
        {
            get => _usedPercent;
            set => SetProperty(ref _usedPercent, value);
        }

        public string MountedOn
        {
            get => _mountedOn;
            set => SetProperty(ref _mountedOn, value);
        }

        public DiskUsageBean(string fileSystem, string size, string used,
            string available, string usedPercent, string mountedOn)
        {
            _fileSystem = fileSystem;
            _size = size;
            _used = used;
            _available = available;
            _usedPercent = usedPercent;
            _mountedOn = mountedOn;
        }
    }
}