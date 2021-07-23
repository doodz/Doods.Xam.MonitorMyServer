using System;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToAptListConverter : SshConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(UpgradableBeanWhapper)) return true;

            if (objectType == typeof(IEnumerable<UpgradableBean>)) return true;
            ;

            return objectType == typeof(UpgradableBean);
        }

        public override object Read(string content, Type objectType)

        {
            var lst = new List<UpgradableBean>();

            //libraspberrypi-bin/stable 1.20170703-1 armhf [upgradable from: 1.20170515-1]
            //                0                 1      2        3        4       5
            var upgradables = content.Split('\n').Where(r =>
                !string.IsNullOrWhiteSpace(r) && !string.IsNullOrEmpty(r) && !r.Contains("Listing"));
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

            if (objectType == typeof(UpgradableBeanWhapper)) return new UpgradableBeanWhapper(lst);

            return lst;
        }
    }
}