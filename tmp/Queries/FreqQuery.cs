﻿using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;
using System;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  vcgencmd measure_clock arm
    ///  frequency(45)=600064000
    /// </example>
    public class FreqQuery : GenericQuery<long>
    {

        public static int FREQ_ARM = 0;
        public static int FREQ_CORE = 1;

        private static string FREQ_CMD = " measure_clock";

        private string _vcgencmdPath;
        private int _unit;

        public FreqQuery(IClientSsh client, string vcgencmdPath, int unit) : base(client)
        {
            _vcgencmdPath = vcgencmdPath;
            _unit = unit;
            CmdString = _vcgencmdPath + FREQ_CMD;
            if (_unit == FREQ_ARM)
            {
                CmdString += " arm";
            }
            else if (_unit == FREQ_CORE)
            {
                CmdString += " core";
            }
        }

        protected override long PaseResult(string result)
        {
            return ParseFrequency(result);
        }

        private long ParseFrequency(string output)
        {
            string[] splitted = output.Trim().Split('=');
            long formatted = 0;
            if (splitted.Length >= 2)
            {
                try
                {
                    formatted = long.Parse(splitted[1]);
                }
                catch (FormatException e)
                {
                    Client.Logger.Error("Could not parse frequency.");
                    Client.Logger.Error(
                            $"Output of 'vcgencmd measure_clock [core/arm]': {Environment.NewLine}{output}");
                    
                }
            }
            else
            {
                Client.Logger.Error("Could not parse frequency.");
                Client.Logger.Error($"Output of 'vcgencmd measure_clock [core/arm]': {Environment.NewLine}{output}");

            }
            return formatted;
        }
    }
}
