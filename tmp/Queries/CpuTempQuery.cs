﻿using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  vcgencmd measure_temp
    ///  temp=44.4'C
    /// </example>
    public class CpuTempQuery : GenericQuery<double>
    {

        private static Regex CPU_PATTERN = new Regex(@"[0-9.]{4,}");
        private static string CPUTEMP_CMD = " measure_temp";

        private string _vcgencmdPath;

        public CpuTempQuery(IClientSsh client, string vcgencmdPath) : base(client)
        {
            _vcgencmdPath = vcgencmdPath;
            CmdString = _vcgencmdPath + CPUTEMP_CMD;
        }

        protected override double PaseResult(string result)
        {
            return parseTemperature(result);
        }

        private double parseTemperature(string output)
        {

            Match match = CPU_PATTERN.Match(output);
            if (match.Success)
            {

                var temperature = double.Parse(match.Value, CultureInfo.InvariantCulture);
                return temperature;
            }

            Client.Logger.Error("Could not parse cpu temperature.");
            Client.Logger.Error($"Output of 'vcgencmd measure_temp': {Environment.NewLine}{output}");
            return 0D;

        }
    }
}
