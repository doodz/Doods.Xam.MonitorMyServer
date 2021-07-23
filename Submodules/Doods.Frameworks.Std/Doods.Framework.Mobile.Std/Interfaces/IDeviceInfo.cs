namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IDeviceInfo
    {
        string Version { get; }

        string Build { get; }
        string ApplicationName { get; }

        string PackageName { get; }
    }
}