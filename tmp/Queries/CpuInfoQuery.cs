using System;
using System.Globalization;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  lscpu
    ///  Architecture:          armv7l
    ///  Byte Order:            Little Endian
    ///  CPU(s):                4
    ///  On-line CPU(s) list:   0-3
    ///  Thread(s) per core:    1
    ///  Core(s) per socket:    4
    ///  Socket(s):             1
    ///  Model name:            ARMv7 Processor rev 5 (v7l)
    ///  CPU max MHz:           900.0000
    ///  CPU min MHz:           600.0000
    /// 
    /// 
    /// 
    ///  lscpu
    ///  Architecture:          x86_64
    ///  CPU op-mode(s):        32-bit, 64-bit
    ///  Byte Order:            Little Endian
    ///  CPU(s):                2
    ///  On-line CPU(s) list:   0,1
    ///  Thread(s) per core:    1
    ///  Core(s) per socket:    2
    ///  Socket(s):             1
    ///  NUMA node(s):          1
    ///  Vendor ID:             AuthenticAMD
    ///  CPU family:            18
    ///  Model:                 1
    ///  Model name:            AMD A4-3300 APU with Radeon(tm) HD Graphics
    ///  Stepping:              0
    ///  CPU MHz:               800.000
    ///  CPU max MHz:           2500.0000
    ///  CPU min MHz:           800.0000
    ///  BogoMIPS:              4999.56
    ///  Virtualization:        AMD-V
    ///  L1d cache:             64K
    ///  L1i cache:             64K
    ///  L2 cache:              512K
    ///  NUMA node0 CPU(s):     0,1
    /// </example>
    public class CpuInfoBean
    {
        public string Architecture;
        public string ByteOrder;
        public int Cpu;
        public string OnlineCpuList;
        public int ThreadPerCore;
        public int CorePerSocket;
        public int Socket;
        public string ModelName;
        public double CpuMaxMHz;
        public double CpuMinMHz;


        public string CpuOpMode;
        public string VendorId;

    }

    public class CpuInfoQuery : GenericQuery<CpuInfoBean>
    {
        public CpuInfoQuery(IClientSsh client) : base(client)
        {
            CmdString = "lscpu";
        }


        protected override CpuInfoBean PaseResult(string result)
        {
            var lines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var res = new CpuInfoBean();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(':');
                var key = line[0].Trim();
                var value = line[1].Trim();
                switch (key)
                {
                    case "Architecture":
                        res.Architecture = value;
                        break;
                    case "Byte Order":
                        res.ByteOrder = value;
                        break;
                    case "CPU(s)":
                        res.Cpu = int.Parse(value);
                        break;
                    case "On - line CPU(s) list":
                        res.OnlineCpuList = value;
                        break;
                    case "Thread(s) per core":
                        res.ThreadPerCore = int.Parse(value);
                        break;
                    case "Core(s) per socket":
                        res.CorePerSocket = int.Parse(value);
                        break;
                    case "Socket(s)":
                        res.Socket = int.Parse(value);
                        break;
                    case "Model name":
                        res.ModelName = value;
                        break;
                    case "CPU max MHz":
                        res.CpuMaxMHz = double.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    case "CPU min MHz":
                        res.CpuMinMHz = double.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    case "CPU op-mode(s)":
                        res.CpuOpMode = value;
                        break;
                    case "Vendor ID:":
                        res.VendorId = value;
                        break;
                }
            }

            return res;
        }
    }
}