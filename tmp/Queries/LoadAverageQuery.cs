using System;
using System.Globalization;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  cat /proc/loadavg; cat /proc/stat | grep cpu | wc -l
    ///  0.06 0.01 0.00 1/116 31124
    ///  5
    /// </example>
    public class LoadAverageQuery : GenericQuery<double>
    {

        private LoadAveragePeriod _period;
        private string LOAD_AVG_CMD = "cat /proc/loadavg; cat /proc/stat | grep cpu | wc -l";
        public LoadAverageQuery(IClientSsh client, LoadAveragePeriod period) : base(client)
        {
            _period = period;
            CmdString = LOAD_AVG_CMD;
        }

        protected override double PaseResult(string result)
        {
            return ParseLoadAverage(result);
        }

        private double ParseLoadAverage(string output)
        {
            var lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var loadAvg = 0D;
            foreach (var line in lines)
            {
                //LOGGER.debug("Checking line: {}", line);
                string[] split = line.Split(' ');
                if (split.Length == 5)
                {
                    try
                    {
                        switch (_period)
                        {
                            case LoadAveragePeriod.OneMinute:
                                loadAvg = double.Parse(split[0], CultureInfo.InvariantCulture);
                                break;
                            case LoadAveragePeriod.FiveMinutes:
                                loadAvg = double.Parse(split[1], CultureInfo.InvariantCulture);
                                break;
                            case LoadAveragePeriod.FifteenMinutes:
                                loadAvg = double.Parse(split[2], CultureInfo.InvariantCulture);
                                break;
                            default:
                                loadAvg = 0D;
                                break;
                                //throw new RuntimeException("Unknown LoadAveragePeriod!");
                        }
                        // got load average, continue with next line
                        continue;
                    }
                    catch (FormatException e)
                    {
                        //LOGGER.debug("Skipping line: {}", line);
                    }
                }
                //if (split.Length == 1 && loadAvg != null)
                //{
                //    // core count line
                //    try
                //    {
                //        Integer coreCount = Integer.parseInt(split[0].trim()) - 1;
                //        return Math.min(1.0D, loadAvg / coreCount);
                //    }
                //    catch (NumberFormatException e)
                //    {
                //        LOGGER.debug("Skipping line: {}", line);
                //    }
                //}
                //LOGGER.debug("Skipping line: {}", line);
            }
            //LOGGER.error("Expected a different output of command: {}", LOAD_AVG_CMD);
            //LOGGER.error("Actual output was: {}", output);
            return loadAvg;
        }
    }
}
