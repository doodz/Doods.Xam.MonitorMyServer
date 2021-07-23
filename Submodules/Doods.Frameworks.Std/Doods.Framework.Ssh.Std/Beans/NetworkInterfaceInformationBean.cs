using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class NetworkInterfaceInformationBean : NotifyPropertyChangedBase
    {
        /// <summary>
        ///     If an interface has no carrier, it is not up and running (no ip adress
        ///     and so on..)
        /// </summary>
        private bool _hasCarrier;

        /// <summary>
        ///     Optional: the ip adress.
        /// </summary>
        private string _ipAdress;

        /// <summary>
        ///     The name of the interface (eth0, wlan0, ...).
        /// </summary>
        private string _name;

        /// <summary>
        ///     Optional: Link and signal quality if a wifi interface.
        /// </summary>
        private WlanBean _wlanInfo;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool HasCarrier
        {
            get => _hasCarrier;
            set => SetProperty(ref _hasCarrier, value);
        }

        public string IpAdress
        {
            get => _ipAdress;
            set => SetProperty(ref _ipAdress, value);
        }

        public WlanBean WlanInfo
        {
            get => _wlanInfo;
            set => SetProperty(ref _wlanInfo, value);
        }
    }
}