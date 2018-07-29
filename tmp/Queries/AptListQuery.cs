using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// 
    /// apt list --upgradable
    /// Listing...Done
    /// libraspberrypi-bin/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi-dev/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi-doc/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi0/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// raspberrypi-bootloader/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// raspberrypi-kernel/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// 
    /// 
    /// 
    /// apt list --upgradable -a
    /// Listing...Done
    /// libraspberrypi-bin/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi-bin/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// libraspberrypi-dev/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi-dev/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// libraspberrypi-doc/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi-doc/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// libraspberrypi0/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// libraspberrypi0/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// raspberrypi-bootloader/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// raspberrypi-bootloader/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// raspberrypi-kernel/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    /// raspberrypi-kernel/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// 
    /// 
    /// </example>
    public class AptListQuery : GenericQuery<IEnumerable<UpgradableBean>>
    {
        public AptListQuery(IClientSsh client) : base(client)
        {
            CmdString = "apt list --upgradable";
        }

        protected override IEnumerable<UpgradableBean> PaseResult(string result)
        {

            var res = result.Split('\n').Where(r => !string.IsNullOrWhiteSpace(r) && !string.IsNullOrEmpty(r) && !r.Contains("Listing"));
            return GetUpgradables(res);
        }


        private IEnumerable<UpgradableBean> GetUpgradables(IEnumerable<string> upgradables)
        {
            var lst = new List<UpgradableBean>();

            //libraspberrypi-bin/stable 1.20170703-1 armhf [upgradable from: 1.20170515-1]
            //                0                 1      2        3        4       5

            foreach (var upgradable in upgradables)
            {
                var res = upgradable.Split().Where(u => !string.IsNullOrWhiteSpace(u) && !string.IsNullOrEmpty(u));

                var obj = new UpgradableBean();
                var str = res.ElementAt(0);
                obj.Name = str.Split('/').First();
                obj.FromRepo = str.Split('/').Last();
                str = res.ElementAt(1);
                obj.NewVersion = str.Split('-').First();
                str = res.ElementAt(2);
                obj.Platform = str;
                str = res.ElementAt(5);
                obj.HoldHold = str.Split('-').First();

                lst.Add(obj);
            }

            return lst;
        }
    }
}
