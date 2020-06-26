using System;
using System.Collections.Generic;
using System.Text;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class History
    {
        public long HostId { get; set; }
        public string HostName { get; set; } 
        public DateTime LastSync { get; set; }
        public DateTime LastUpdate { get; set; }
        public int NombrerPackargeCanBeUpdted { get; set; }

    }
}
