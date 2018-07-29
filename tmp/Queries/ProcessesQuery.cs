using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  ps -U root -u root -N
    ///  PID TTY          TIME CMD
    ///  388 ?        00:00:27 avahi-daemon
    ///  394 ?        00:00:00 dbus-daemon
    ///  400 ?        00:00:00 avahi-daemon
    ///  451 ?        00:07:58 openvpn
    ///  493 ?        00:00:08 thd
    ///  526 ?        00:10:36 pihole-FTL
    ///  624 ?        00:01:31 ntpd
    ///  682 ?        00:00:41 lighttpd
    ///  697 ?        00:00:00 php-cgi
    ///  698 ?        00:00:00 php-cgi
    ///  699 ?        00:00:00 php-cgi
    ///  700 ?        00:00:00 php-cgi
    ///  701 ?        00:00:00 php-cgi
    ///  1065 ?        00:00:00 sshd
    ///  1067 pts/0    00:00:00 bash
    ///  1095 pts/0    00:00:00 ps
    ///  27579 ?        00:00:04 dnsmasq
    /// </example>
    public class ProcessesQuery : GenericQuery<IEnumerable<ProcessBean>>
    {

        private string PROCESS_NO_ROOT_CMD = "ps -U root -u root -N";
        private string PROCESS_ALL = "ps -A";

        public ProcessesQuery(IClientSsh client, bool showRootProcesses) : base(client)
        {
            CmdString = showRootProcesses ? PROCESS_ALL : PROCESS_NO_ROOT_CMD;
        }

        protected override IEnumerable<ProcessBean> PaseResult(string result)
        {
            return ParseProcesses(result);
        }

        /// <summary>
        /// Parses the output of the ps command.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private IEnumerable<ProcessBean> ParseProcesses(string output)
        {
            //var lines = output.Split('\n');

            var lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var processes = new List<ProcessBean>();
            var count = 0;
            foreach (var line in lines)
            {
                if (count == 0)
                {
                    // first line
                    count++;
                    continue;
                }
                // split line at whitespaces

                var cols = line.Trim().Split().Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                if (cols.Length >= 4)
                {
                    try
                    {
                        // command may contain whitespace, so join again
                        var sb = new StringBuilder();

                        var cmd = string.Join(" ", cols);

                        processes.Add(new ProcessBean(
                            int.Parse(cols[0]), cols[1], cols[2],cols[3], cmd));
                    }
                    catch (FormatException e)
                    {
                        Client.Logger.Error("Could not parse processes.");
                        Client.Logger.Error($"Error occured on following line: {line}");
                    }
                }
                else
                {
                    Client.Logger.Error($"Line[] length: {cols.Length}");
                    Client.Logger.Error($"Expcected another output of ps. Skipping line: {line}");
                        
                }
            }
            return processes;
        }
    }
}