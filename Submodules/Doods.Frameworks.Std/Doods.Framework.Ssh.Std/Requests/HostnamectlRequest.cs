namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     ~# hostnamectl
    ///     Static hostname: openmediavault5
    ///     Icon name: computer-vm
    ///     Chassis: vm
    ///     Machine ID: 5***************************9
    ///     Boot ID: 1******************************4
    ///     Virtualization: microsoft
    ///     Operating System: Debian GNU/Linux 10 (buster)
    ///     Kernel: Linux 5.8.0-0.bpo.2-amd64
    ///     Architecture: x86-64
    /// </example>
    public class HostnamectlRequest : SshRequestBase
    {
        public const string RequestString = "hostnamectl";

        public HostnamectlRequest() : base(RequestString)
        {
        }
    }
}