using System;
using System.Text;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     vcgencmd version
    ///     Apr  5 2017 11:49:52
    ///     Copyright(c) 2012 Broadcom
    ///     version 3ca4cf4a663c5351eaec08b29d50d6e8324981b4(clean) (release)
    /// </example>
    public class FirmwareQuery : GenericQuery<string>
    {
        private readonly string _vcgencmdPath;
        private readonly char BLANK = ' ';

        private readonly int SHORTENED_HASH_LENGTH = 8;

        public FirmwareQuery(IClientSsh client, string vcgencmdPath) : base(client)
        {
            _vcgencmdPath = vcgencmdPath;
            CmdString = _vcgencmdPath + " version";
        }

        protected override string PaseResult(string result)
        {
            return ParseFirmwareVersion(result);
        }

        private string ParseFirmwareVersion(string output)
        {
            var splitted = output.Split('\n');
            if (splitted.Length >= 3)
            {
                if (splitted[2].StartsWith("version "))
                    return checkAndFormatVersionHash(splitted[2].Replace("version ", ""));
                //return splitted[2].replace("version ", "");
                return splitted[2];
            }

            Client.Logger.Error("Could not parse firmware. Maybe the output of 'vcgencmd version' changed.");
            Client.Logger.Error($"Output of 'vcgencmd version': {Environment.NewLine}{output}");
            return "n/a";
        }

        private string checkAndFormatVersionHash(string versionString)
        {
            var sb = new StringBuilder();
            var splitted = versionString.Split(BLANK);
            if (splitted.Length == 3)
            {
                var hash = splitted[0];
                if (hash.Length > SHORTENED_HASH_LENGTH)
                    sb.Append(hash.Substring(0, SHORTENED_HASH_LENGTH));
                else
                    sb.Append(hash);
                return sb.Append(BLANK).Append(splitted[1]).Append(BLANK).Append(splitted[2]).ToString();
            }

            return versionString;
        }
    }
}