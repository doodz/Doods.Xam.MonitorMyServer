namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     lscpu
    ///     Architecture:          armv7l
    ///     Byte Order:            Little Endian
    ///     CPU(s):                4
    ///     On-line CPU(s) list:   0-3
    ///     Thread(s) per core:    1
    ///     Core(s) per socket:    4
    ///     Socket(s):             1
    ///     Model name:            ARMv7 Processor rev 5 (v7l)
    ///     CPU max MHz:           900.0000
    ///     CPU min MHz:           600.0000
    ///     lscpu
    ///     Architecture:          x86_64
    ///     CPU op-mode(s):        32-bit, 64-bit
    ///     Byte Order:            Little Endian
    ///     CPU(s):                2
    ///     On-line CPU(s) list:   0,1
    ///     Thread(s) per core:    1
    ///     Core(s) per socket:    2
    ///     Socket(s):             1
    ///     NUMA node(s):          1
    ///     Vendor ID:             AuthenticAMD
    ///     CPU family:            18
    ///     Model:                 1
    ///     Model name:            AMD A4-3300 APU with Radeon(tm) HD Graphics
    ///     Stepping:              0
    ///     CPU MHz:               800.000
    ///     CPU max MHz:           2500.0000
    ///     CPU min MHz:           800.0000
    ///     BogoMIPS:              4999.56
    ///     Virtualization:        AMD-V
    ///     L1d cache:             64K
    ///     L1i cache:             64K
    ///     L2 cache:              512K
    ///     NUMA node0 CPU(s):     0,1
    /// </example>
    public class CpuInfoRequest : SshRequestBase
    {
        public const string RequestString = "lscpu";

        public CpuInfoRequest() : base(RequestString)
        {
        }
    }
}