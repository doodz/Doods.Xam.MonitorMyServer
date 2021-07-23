using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToLastloginConverter : SshConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<LastloginBean>);
        }

        public override object Read(string content, Type objectType)
        {
            var lst = new List<LastloginBean>();
            var lines = content.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);


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