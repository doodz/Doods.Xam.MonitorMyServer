using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     /sbin/ifconfig  eth0 | grep "inet addr"
    ///     inet addr:192.168.1.73  Bcast:192.168.1.255  Mask:255.255.255.0
    /// </example>
    public class IfConfigQuery : GenericQuery<string>
    {
        private readonly Regex IPADDRESS_PATTERN =
            new Regex(
                "\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b");

        public IfConfigQuery(IClientSsh client, string name) : base(client)
        {
            CmdString = "/sbin/ifconfig " + name + " | grep \"inet addr\"";
        }

        protected override string PaseResult(string result)
        {
            //LOGGER.debug("Output of ifconfig query: {}.", ifconfigOutput);
            var match = IPADDRESS_PATTERN.Match(result);
            if (match.Success)
            {
                //String ipAddress = m2.group().trim();
                var ipAddress = match.Value.Trim();
                return ipAddress;
            }

            Client.Logger.Error($"IP address pattern: No match found for output: {result}.");
            Client.Logger.Error("Querying ip address failed. It seems like 'ip' and 'ifconfig' are not available.");
            return null;
        }
    }
}