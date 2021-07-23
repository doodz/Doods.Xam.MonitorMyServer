namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    ///     Get logs files from /var/log
    /// </summary>
    /// <example>
    ///     find /var/log/ -type f  ! -iname "*.gz" ! -iname "*.xz" ! -iname "*.[0-9]"
    ///     /var/log/installer/hardware-summary
    ///     /var/log/installer/lsb-release
    ///     /var/log/installer/syslog
    ///     /var/log/installer/cdebconf/questions.dat
    ///     /var/log/installer/cdebconf/templates.dat
    ///     /var/log/installer/partman
    ///     /var/log/installer/status
    ///     /var/log/libvirt/lxc/.placeholder
    ///     /var/log/libvirt/uml/.placeholder
    ///     /var/log/libvirt/qemu/.placeholder
    ///     /var/log/btmp
    ///     /var/log/salt/minion
    ///     /var/log/debug
    ///     /var/log/syslog
    ///     /var/log/mail.info
    ///     /var/log/faillog
    ///     /var/log/wtmp
    ///     /var/log/cron-apt/lastfullmessage
    ///     /var/log/cron-apt/log
    ///     /var/log/lastlog
    ///     /var/log/php7.3-fpm.log
    ///     /var/log/alternatives.log
    ///     /var/log/fontconfig.log
    ///     /var/log/mail.warn
    ///     /var/log/mail.log
    ///     /var/log/mail.err
    ///     /var/log/nginx/openmediavault-webgui_error.log
    ///     /var/log/nginx/error.log
    ///     /var/log/nginx/access.log
    ///     /var/log/nginx/openmediavault-webgui_access.log
    ///     /var/log/apt/term.log
    ///     /var/log/apt/history.log
    ///     /var/log/monit.log
    ///     /var/log/kern.log
    ///     /var/log/user.log
    ///     /var/log/bootstrap.log
    ///     /var/log/daemon.log
    ///     /var/log/messages
    ///     /var/log/samba/log.smbd
    ///     /var/log/auth.log
    ///     /var/log/tallylog
    ///     /var/log/proftpd/proftpd.log
    ///     /var/log/proftpd/controls.log
    ///     /var/log/dpkg.log
    /// </example>
    public class LogsFilesRequest : SshRequestBase
    {
        public const string RequestString =
            "find /var/log/ -type f  ! -iname \"*.gz\" ! -iname \"*.xz\" ! -iname \"*.[0-9]\"";

        public LogsFilesRequest() : base(RequestString)
        {
        }
    }
}