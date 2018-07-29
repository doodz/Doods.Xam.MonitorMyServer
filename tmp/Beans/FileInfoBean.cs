using Doods.Framework.Std;
using System;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class FileInfoBean : NotifyPropertyChangedBase
    {
        /// <summary>
        /// -rw-r--r--
        /// </summary>
        public string AccessRights { get; set; }

        public int Id { get; set; }

        public string Owner { get; set; }
        public string Group { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsFolder { get; set; }


        public string FullPath => $"{Path}/{Name}";
    }
}
