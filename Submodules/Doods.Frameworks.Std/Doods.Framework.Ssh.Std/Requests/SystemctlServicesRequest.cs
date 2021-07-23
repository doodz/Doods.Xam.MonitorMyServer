namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     systemctl list-units --type=service
    ///     UNIT LOAD   ACTIVE SUB     DESCRIPTION
    ///     avahi-daemon.service loaded active running Avahi mDNS/DNS-SD Stack
    ///     blk-availability.service loaded active exited  Availability of block devices
    ///     collectd.service loaded active running Statistics collection and monitoring daemon
    ///     console-setup.service loaded active exited  Set console font and keymap
    ///     cpufrequtils.service                                                                      loaded active exited LSB:
    ///     set CPUFreq kernel parameters
    ///     cron.service loaded active running Regular background program processing daemon
    ///     dbus.service loaded active running D-Bus System Message Bus
    ///     getty @tty1.service loaded active running Getty on tty1
    ///     ifup@ens3.service loaded active exited  ifup for ens3
    ///     ifupdown-pre.service loaded active exited  Helper to synchronize boot up for ifupdown
    ///     keyboard-setup.service loaded active exited  Set the console keyboard layout
    ///     kmod-static-nodes.service loaded active exited  Create list of required static device nodes for the current kernel
    ///     libvirt-guests.service loaded active exited  Suspend/Resume Running libvirt Guests
    ///     libvirtd.service loaded active running Virtualization daemon
    ///     loadcpufreq.service loaded active exited  LSB: Load kernel modules needed to enable cpufreq scaling
    ///     lvm2-monitor.service loaded active exited  Monitoring of LVM2 mirrors, snapshots etc. using dmeventd or progress
    ///     polling
    ///     ● mdmonitor.service loaded failed failed  MD array monitor
    ///     ...
    ///     ...
    ///     LOAD   = Reflects whether the unit definition was properly loaded.
    ///     ACTIVE = The high-level unit activation state, i.e.generalization of SUB.
    ///     SUB    = The low-level unit activation state, values depend on unit type.
    ///     65 loaded units listed.Pass --all to see loaded but inactive units, too.
    ///     To show all installed unit files use 'systemctl list-unit-files'.
    /// </example>
    public class SystemctlServicesRequest : SshRequestBase
    {
        public const string RequestString = "systemctl list-units --type=service";

        public SystemctlServicesRequest() : base(RequestString)
        {
        }
    }
}