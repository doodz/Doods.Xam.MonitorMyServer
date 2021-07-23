using Doods.Framework.Mobile.Std.Interfaces;
using Plugin.DeviceInfo;

namespace Doods.Framework.Mobile.Std
{
    internal class MyDeviceInfo : IDeviceInfo
    {
        public MyDeviceInfo()
        {
            var res = CrossDeviceInfo.Current;


            Version = res.Version;
            Build = res.AppBuild;
        }

        public string Version { get; }
        public string Build { get; }
        public string ApplicationName => string.Empty;
        public string PackageName => string.Empty;
    }
}