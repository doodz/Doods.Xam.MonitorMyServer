using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class DiskUsage : NotifyPropertyChangedBase
    {
        private string _available;
        private string _fileSystem;
        private string _mountedOn;
        private string _size;
        private string _used;
        private string _usedPercent;


        public string FileSystem
        {
            get => _fileSystem;
            internal set => SetProperty(ref _fileSystem, value);
        }

        public string Size
        {
            get => _size;
            internal set => SetProperty(ref _size, value);
        }

        public string Used
        {
            get => _used;
            internal set => SetProperty(ref _used, value);
        }

        public string Available
        {
            get => _available;
            internal set => SetProperty(ref _available, value);
        }

        public string UsedPercent
        {
            get => _usedPercent;
            internal set => SetProperty(ref _usedPercent, value);
        }

        public string MountedOn
        {
            get => _mountedOn;
            internal set => SetProperty(ref _mountedOn, value);
        }


        public string UseSize => $"{_used} / {_size}";
        public string AvailableUsedPercent => $"{_available} ( {_usedPercent} )";

        public float UsedPercentNoUnit => float.Parse(_usedPercent.Replace('%', ' '));

        //public DiskUsage(string fileSystem, string size, string used,
        //    string available, string usedPercent, string mountedOn)
        //{
        //    _fileSystem = fileSystem;
        //    _size = size;
        //    _used = used;
        //    _available = available;
        //    _usedPercent = usedPercent;
        //    _mountedOn = mountedOn;
        //}
    }
}