// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SshToHostnamectlConverter.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/01/12 at 17:48: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToHostnamectlConverter : SshConverter
    {
        private readonly Regex _pattern = new Regex(@"^(.*):\s*(.*)");

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HostnamectlBean);
        }

        public override object Read(string content, Type objectType)
        {
            var lines = content.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);

            var res = new HostnamectlBean();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(':');
                var key = line[0].Trim();
                var value = line[1].Trim();
                switch (key)
                {
                    case "Static hostname":
                        res.StaticHostname = value;
                        break;
                    case "Icon name":
                        res.IconName = value;
                        break;
                    case "Chassis":
                        res.Chassis = value;
                        break;
                    case "Machine ID":
                        res.MachineID = value;
                        break;
                    case "Boot ID":
                        res.BootID = value;
                        break;
                    case "Virtualization":
                        res.Virtualization = value;
                        break;
                    case "Operating System":
                        res.OperatingSystem = value;
                        break;
                    case "Kernel":
                        res.Kernel = value;
                        break;
                    case "Architecture":
                        res.Architecture = value;
                        break;
                }
            }

            return res;
        }
    }
}