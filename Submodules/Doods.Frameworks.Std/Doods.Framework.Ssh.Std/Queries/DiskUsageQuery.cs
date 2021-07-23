using System.Collections.Generic;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Converters;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Ssh.Std.Requests;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     LC_ALL=C df -h
    ///     Filesystem Size  Used Avail Use% Mounted on
    ///     /dev/root       7.2G  1.2G  5.8G  17% /
    ///     devtmpfs        459M     0  459M   0% /dev
    ///     tmpfs           463M     0  463M   0% /dev/shm
    ///     tmpfs           463M  6.2M  457M   2% /run
    ///     tmpfs           5.0M  4.0K  5.0M   1% /run/lock
    ///     tmpfs           463M     0  463M   0% /sys/fs/cgroup
    ///     /dev/mmcblk0p1   63M   21M   42M  34% /boot
    /// </example>
    public class DiskUsageQuery : GenericQuery<IEnumerable<DiskUsageBean>>
    {
        public DiskUsageQuery(IClientSsh client) : base(client)
        {
            CmdString = DiskUsageRequest.RequestString;
        }

        protected override IEnumerable<DiskUsageBean> PaseResult(string result)
        {
            return (IEnumerable<DiskUsageBean>) new SshToDiskUsageConverter().Read(result,
                typeof(IEnumerable<DiskUsageBean>));
        }
    }
}