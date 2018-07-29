using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;
using System;
using System.Collections.Generic;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  cat /proc/meminfo | tr -s " "
    ///  MemTotal: 947732 kB
    ///  MemFree: 634892 kB
    ///  MemAvailable: 843896 kB
    ///  Buffers: 45752 kB
    ///  Cached: 208548 kB
    ///  SwapCached: 0 kB
    ///  Active: 203908 kB
    ///  Inactive: 73028 kB
    ///  Active(anon): 22844 kB
    ///  Inactive(anon): 11356 kB
    ///  Active(file): 181064 kB
    ///  Inactive(file): 61672 kB
    ///  Unevictable: 0 kB
    ///  Mlocked: 0 kB
    ///  SwapTotal: 102396 kB
    ///  SwapFree: 102396 kB
    ///  Dirty: 4 kB
    ///  Writeback: 0 kB
    ///  AnonPages: 22580 kB
    ///  Mapped: 29492 kB
    ///  Shmem: 11568 kB
    ///  Slab: 22932 kB
    ///  SReclaimable: 14452 kB
    ///  SUnreclaim: 8480 kB
    ///  KernelStack: 944 kB
    ///  PageTables: 964 kB
    ///  NFS_Unstable: 0 kB
    ///  Bounce: 0 kB
    ///  WritebackTmp: 0 kB
    ///  CommitLimit: 576260 kB
    ///  Committed_AS: 170996 kB
    ///  VmallocTotal: 1114112 kB
    ///  VmallocUsed: 0 kB
    ///  VmallocChunk: 0 kB
    ///  CmaTotal: 8192 kB
    ///  CmaFree: 3728 kB
    /// </example>
    public class MemoryQuery : GenericQuery<OsMemoryBean>
    {
        public string MEMORY_INFO_CMD = "cat /proc/meminfo | tr -s \" \"";
        public string MEMORY_UNKNOWN_OUPUT = "Memory information could not be queried. See the log for details.";

        private string KEY_TOTAL = "MemTotal:";
        private string KEY_AVAILABLE = "MemAvailable:";
        private string KEY_FREE = "MemFree:";
        private string KEY_BUFFERS = "Buffers:";
        private string KEY_CACHED = "Cached:";
        public MemoryQuery(IClientSsh client) : base(client)
        {
            CmdString = MEMORY_INFO_CMD;
        }

        protected override OsMemoryBean PaseResult(string result)
        {
            return FormatMemoryInfo(result);
        }
        private OsMemoryBean FormatMemoryInfo(string output)
        {
            var memoryData = new Dictionary<string, long>();
            //var res = Regex.Matches(output, "[\r\n]+").Cast<Match>() .ToArray();
            var res = output.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in res)
            {
                var paragraphs = line.Split(' ');
                if (paragraphs.Length > 1)
                {
                    memoryData.Add(paragraphs[0], long.Parse(paragraphs[1]));
                }
            }

            if (memoryData.TryGetValue(KEY_TOTAL, out long memTotal))
            {
                if (memoryData.TryGetValue(KEY_AVAILABLE, out long memAvailable))
                {
                    //LOGGER.debug("Using MemAvailable for calculation of free memory.");
                    return new OsMemoryBean(memTotal, memTotal - memAvailable);
                }
                // maybe Linux Kernel < 3.14
                // estimate "used": MemTotal - (MemFree + Buffers + Cached)
                // thats also how 'free' is doing it
                var memFree = memoryData.ContainsKey(KEY_FREE);
                var memCached = memoryData.ContainsKey(KEY_CACHED);
                var memBuffers = memoryData.ContainsKey(KEY_BUFFERS);
                if (memFree && memCached && memBuffers)
                {
                    var memUsed = memTotal - (memoryData[KEY_FREE] + memoryData[KEY_BUFFERS] + memoryData[KEY_CACHED]);
                    //LOGGER.debug("Using MemFree,Buffers and Cached for calculation of free memory.");
                    return new OsMemoryBean(memTotal, memUsed);
                }

            }

            return ProduceError(output);

        }

        private OsMemoryBean ProduceError(string output)
        {
            Client.Logger.Error($"Expected a different output of command: {MEMORY_INFO_CMD}");
            Client.Logger.Error($"Output was : {output}");
            return new OsMemoryBean(MEMORY_UNKNOWN_OUPUT);
        }
    }
}
