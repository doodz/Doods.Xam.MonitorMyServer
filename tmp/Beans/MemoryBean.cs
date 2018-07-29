using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Std;
using System;
using System.Linq;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class MemoryBean : NotifyPropertyChangedBase
    {
        private long _bytes;

        public long Bytes
        {
            get => _bytes;
            set => SetProperty(ref _bytes, value);
        }

        private MemoryBean(long bytes)
        {
            _bytes = bytes;
        }

        public static MemoryBean From(Memory scale, long data)
        {
            var memoryBean = new MemoryBean(data);
            if (scale == Memory.B)
                memoryBean.Bytes = data * Memory.B.Scale;
            else if (scale == Memory.KB)
                memoryBean.Bytes = data * Memory.KB.Scale;
            else if (scale == Memory.MB)
                memoryBean.Bytes = data * Memory.MB.Scale;
            else if (scale == Memory.GB)
                memoryBean.Bytes = data * Memory.GB.Scale;
            else if (scale == Memory.TB)
                memoryBean.Bytes = data * Memory.GB.Scale;
            return memoryBean;
        }

        public string HumanReadableByteCount(bool si)
        {

            
            var unit = si ? 1000 : 1024;
            if (Bytes < unit)
                return Bytes + " B";
            var exp = (int) (Math.Log(Bytes) / Math.Log(unit));
            var pre = (si ? "kMGTPE" : "KMGTPE").ElementAt(exp - 1)
                      + (si ? "" : "i");
            return string.Format("{0} {1}", Bytes / Math.Pow(unit, exp), pre);
        }
    }
}