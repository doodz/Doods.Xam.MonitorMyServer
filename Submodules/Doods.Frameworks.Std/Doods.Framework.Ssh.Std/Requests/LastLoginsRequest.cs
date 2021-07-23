using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     last -a | head -3
    ///     root pts/0        Sun Sep 24 10:01   still logged in    desktop-fpk3rti
    ///     reboot   system boot  Sat Sep 23 20:54 - 10:14  (13:20)     4.9.0-0.bpo.3-amd64
    ///     root     pts/0        Tue Sep 12 10:42 - 17:34  (06:52)     desktop-fpk3rti
    /// </example>
    public class LastLoginsRequest : SshRequestBase
    {
        public const string RequestString = "last -a | head -3";

        public LastLoginsRequest() : base(RequestString)
        {
        }

        public LastLoginsRequest(SshSerializerSettings settings) : base(RequestString, settings)
        {
        }
    }
}