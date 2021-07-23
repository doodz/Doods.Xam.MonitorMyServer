namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     ls -1 /sys/class/net
    ///     eth0
    ///     lo
    ///     tun0
    ///     wlan0
    /// </remarks>
    public class InterfacesRequest : SshRequestBase
    {
        public const string RequestString = "ls -1 /sys/class/net";

        public InterfacesRequest() : base(RequestString)
        {
        }
    }
}