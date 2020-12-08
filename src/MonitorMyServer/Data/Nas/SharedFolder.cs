using System;
using System.Collections.Generic;
using System.Text;

namespace Doods.Xam.MonitorMyServer.Data.Nas
{
    public class SharedFolder
    {
      
        public string Name { get; set; }
        public string Type { get; set; }
        public string Volume { get; set; }
        public string Description { get; set; }
        public Guid Uuid { get; set; }
    }
}
