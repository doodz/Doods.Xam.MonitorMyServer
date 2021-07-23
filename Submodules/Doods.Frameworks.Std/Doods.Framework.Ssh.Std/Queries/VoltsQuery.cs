using System;
using System.Globalization;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class VoltsQuery : GenericQuery<double>
    {
        private readonly string _vcgencmdPath;
        private readonly string VOLTS_CMD = " measure_volts core";

        public VoltsQuery(IClientSsh client, string vcgencmdPath) : base(client)
        {
            _vcgencmdPath = vcgencmdPath;
            CmdString = _vcgencmdPath + VOLTS_CMD;
        }

        protected override double PaseResult(string result)
        {
            return FormatVolts(result);
        }

        private double FormatVolts(string output)
        {
            var splitted = output.Trim().Split('=');
            if (splitted.Length >= 2)
            {
                var voltsWithUnit = splitted[1];
                var volts = voltsWithUnit.Substring(0,
                    voltsWithUnit.Length - 1);


                if (double.TryParse(volts, NumberStyles.Float, CultureInfo.InvariantCulture, out var res2)) return res2;
            }

            Client.Logger.Error("Could not parse cpu voltage.");
            Client.Logger.Error($"Output of 'vcgencmd measure_volts core': {Environment.NewLine}{output}");
            return 0D;
        }
    }
}