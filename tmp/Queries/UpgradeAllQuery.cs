using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// sudo apt-get upgrade libdns100
    /// Reading package lists... Done
    /// Building dependency tree
    /// Reading state information...Done
    /// Calculating upgrade... Some packages could not be installed. This may mean that you have
    /// requested an impossible situation or if you are using the unstable
    /// distribution that some required packages have not yet been created
    /// or been moved out of Incoming.
    /// The following information may help to resolve the situation:
    ///
    ///
    /// The following packages have unmet dependencies:
    ///
    /// bind9-host : Depends: libdns100 (= 1:9.9.5.dfsg-9+deb8u11) but 1:9.9.5.dfsg-9+deb8u12 is to be installed
    /// dnsutils : Depends: libdns100(= 1:9.9.5.dfsg-9+deb8u11) but 1:9.9.5.dfsg-9+deb8u12 is to be installed
    /// E: Broken packages
    ///
    /// ----------------------------------------------------------------------------------------------------------------
    /// 
    /// sudo apt-get upgrade -sy
    /// Reading package lists...Done
    /// Building dependency tree
    /// Reading state information...Done
    /// Calculating upgrade...Done
    /// The following packages will be upgraded:
    /// bind9-host dnsutils libbind9-90 libdns-export100 libdns100 libgcrypt20
    /// libirs-export91 libisc-export95 libisc95 libisccc90 libisccfg-export90 libisccfg90
    /// liblwres90 libraspberrypi-bin libraspberrypi-dev libraspberrypi-doc libraspberrypi0
    /// raspberrypi-bootloader raspberrypi-kernel
    /// 19 upgraded, 0 newly installed, 0 to remove and 0 not upgraded.
    /// ...
    /// Conf libraspberrypi-doc (1.20170703-1 Raspberry Pi Foundation:stable [armhf])
    /// Conf raspberrypi-kernel(1.20170703-1 Raspberry Pi Foundation:stable[armhf])
    /// Conf libraspberrypi-bin(1.20170703-1 Raspberry Pi Foundation:stable[armhf])
    /// </example>
    public class UpgradeAllQuery : GenericQuery<bool>
    {
        public static readonly string Query = "sudo apt-get upgrade -y";
        // $"sudo bash -c 'exec  apt-get upgrade -y' && echo \"{ReturnQuery.ResultOk}\" || echo \"{ReturnQuery.ResultKo}\""


        public UpgradeAllQuery(IClientSsh client) : base(client)
        {
            CmdString = Query;
        }

        protected override bool PaseResult(string result)
        {
            var res = result.Split('\n').Where(r => !string.IsNullOrWhiteSpace(r) && !string.IsNullOrEmpty(r));
            return Parse(res);
        }

        private bool Parse(IEnumerable<string> str)
        {
            return !str.Any(s => s.Contains("E:"));
        }
    }
}