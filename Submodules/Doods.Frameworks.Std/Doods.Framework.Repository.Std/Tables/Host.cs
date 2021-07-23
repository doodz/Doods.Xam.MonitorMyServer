using System.Diagnostics;

namespace Doods.Framework.Repository.Std.Tables
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public class Host : TableBase
    {
        /// <summary>
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// </summary>
        public bool IsOmvServer { get; set; }

        /// <summary>
        /// </summary>
        public bool IsSynoServer { get; set; }

        /// <summary>
        /// </summary>
        public bool IsWebminServer { get; set; }

        /// <summary>
        /// </summary>
        public bool IsRpi { get; set; }

        /// <summary>
        /// </summary>
        public bool IsSsh { get; set; }

        public string Description => $"{UserName}@{Url}:{Port}";

        protected string DebuggerDisplay()
        {
            return
                $"HostName:{HostName})";
        }
    }
}