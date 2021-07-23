// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SshToProcessConverter.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2019
//  </copyright>
//  History:
//   2019/08/06 at 13:32:  aka therv.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToProcessConverter : SshConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<ProcessBean>);
        }

        public override object Read(string content, Type objectType)
        {
            var lines = content.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
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
                            int.Parse(cols[0]), cols[1], cols[2], cols[3], cmd));
                    }
                    catch (FormatException)
                    {
                        //Client.Logger.Error("Could not parse processes.");
                        //Client.Logger.Error($"Error occured on following line: {line}");
                    }
                }
            }

            //if (!objectType.IsArray)
            //{
            //    if (objectType == typeof(Collection<ProcessBean>))
            //    {
            //        return new Collection<ProcessBean>(processes);
            //    }

            //    return lines.ToList();


            //}

            return processes;
        }
    }
}