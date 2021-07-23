namespace Doods.Framework.Ssh.Std.Beans
{
    public class HostnamectlBean
    {
        public string StaticHostname { get; internal set; }
        public string IconName { get; internal set; }

        public string Chassis { get; internal set; }


        public string MachineID { get; internal set; }
        public string BootID { get; internal set; }
        public string Virtualization { get; internal set; }

        public string OperatingSystem { get; internal set; }
        public string Kernel { get; internal set; }
        public string Architecture { get; internal set; }
    }
}