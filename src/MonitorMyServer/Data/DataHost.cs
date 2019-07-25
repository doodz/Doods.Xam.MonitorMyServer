using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class DataHost : NotifyPropertyChangedBase
    {
        private string _displayName;
        private string _id;

        private string _iPAddress;

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public string IPAddress
        {
            get => _iPAddress;
            set => SetProperty(ref _iPAddress, value);
        }
    }
}