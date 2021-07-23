using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     ps -U root -u root -N
    ///     PID TTY          TIME CMD
    ///     388 ?        00:00:27 avahi-daemon
    ///     394 ?        00:00:00 dbus-daemon
    ///     400 ?        00:00:00 avahi-daemon
    ///     451 ?        00:07:58 openvpn
    ///     493 ?        00:00:08 thd
    ///     526 ?        00:10:36 pihole-FTL
    ///     624 ?        00:01:31 ntpd
    ///     682 ?        00:00:41 lighttpd
    ///     697 ?        00:00:00 php-cgi
    ///     698 ?        00:00:00 php-cgi
    ///     699 ?        00:00:00 php-cgi
    ///     700 ?        00:00:00 php-cgi
    ///     701 ?        00:00:00 php-cgi
    ///     1065 ?        00:00:00 sshd
    ///     1067 pts/0    00:00:00 bash
    ///     1095 pts/0    00:00:00 ps
    ///     27579 ?        00:00:04 dnsmasq
    /// </example>
    public class ProcessesRequest : SshRequestBase
    {
        public const string RequestStringNoRoot = "ps -U root -u root -N";
        public const string RequestString = "ps -A";

        public ProcessesRequest(bool showRootProcesses = true) : base(showRootProcesses
            ? RequestString
            : RequestStringNoRoot)
        {
        }

        public ProcessesRequest(SshSerializerSettings settings, bool showRootProcesses = true) : base(
            showRootProcesses ? RequestString : RequestStringNoRoot, settings)
        {
        }
    }
}