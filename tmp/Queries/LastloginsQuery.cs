using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// last -a | head -3
    /// root pts/0        Sun Sep 24 10:01   still logged in    desktop-fpk3rti
    /// reboot   system boot  Sat Sep 23 20:54 - 10:14  (13:20)     4.9.0-0.bpo.3-amd64
    /// root     pts/0        Tue Sep 12 10:42 - 17:34  (06:52)     desktop-fpk3rti
    /// </example>
    public class LastloginsQuery : GenericQuery<IEnumerable<LastloginBean>>
    {
        public LastloginsQuery(IClientSsh client) : base(client)
        {
            CmdString = "last -a | head -3";
        }

        protected override IEnumerable<LastloginBean> PaseResult(string result)
        {
            var lst = new List<LastloginBean>();
            var lines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);


            var pattern = @"\s{2,}|\s-\s";

            foreach (var line in lines)
            {
                var splitsStrings = Regex.Split(line, pattern);

                var l = new LastloginBean
                {
                    UserName = splitsStrings[0],
                    LogedOn = splitsStrings[1],
                    Date = splitsStrings[2]
                };
                if (splitsStrings.Length == 6)
                {
                    l.StillLogged = splitsStrings[3];
                    l.LogedIn = splitsStrings[4];
                    l.LogedFrom = splitsStrings[5];
                }
                else if (splitsStrings.Length == 5)
                {
                    l.StillLogged = splitsStrings[3];
                    l.LogedFrom = splitsStrings[4];
                }


                lst.Add(l);
            }

            return lst;
        }
    }
}