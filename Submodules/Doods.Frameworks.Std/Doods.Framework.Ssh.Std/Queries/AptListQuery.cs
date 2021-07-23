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
    ///     apt list --upgradable
    ///     Listing...Done
    ///     libraspberrypi-bin/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi-dev/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi-doc/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi0/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     raspberrypi-bootloader/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     raspberrypi-kernel/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     apt list --upgradable -a
    ///     Listing...Done
    ///     libraspberrypi-bin/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi-bin/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    ///     libraspberrypi-dev/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi-dev/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    ///     libraspberrypi-doc/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi-doc/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    ///     libraspberrypi0/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     libraspberrypi0/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    ///     raspberrypi-bootloader/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     raspberrypi-bootloader/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    ///     raspberrypi-kernel/stable 1.20170703-1 armhf[upgradable from: 1.20170515 - 1]
    ///     raspberrypi-kernel/now 1.20170515-1 armhf[installed, upgradable to: 1.20170703 - 1]
    /// </example>
    public class AptListQuery : GenericQuery<IEnumerable<UpgradableBean>>
    {
        public AptListQuery(IClientSsh client) : base(client)
        {
            CmdString = UpgradableRequest.RequestString;
        }

        protected override IEnumerable<UpgradableBean> PaseResult(string result)
        {
            return (IEnumerable<UpgradableBean>) new SshToAptListConverter().Read(result, typeof(UpgradableBean));
        }
    }
}