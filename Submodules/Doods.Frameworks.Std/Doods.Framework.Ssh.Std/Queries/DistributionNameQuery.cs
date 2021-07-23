using System;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     cat /etc/*-release | grep PRETTY_NAME
    ///     PRETTY_NAME="Raspbian GNU/Linux 8 (jessie)"
    /// </example>
    public class DistributionNameQuery : GenericQuery<string>
    {
        private static readonly string N_A = "n/a";
        private static readonly string DISTRIBUTION_CMD = "cat /etc/*-release | grep PRETTY_NAME";

        public DistributionNameQuery(IClientSsh client) : base(client)
        {
            CmdString = DISTRIBUTION_CMD;
        }

        protected override string PaseResult(string result)
        {
            return ParseDistribution(result);
        }

        private string ParseDistribution(string output)
        {
            var split = output.Trim().Split('=');
            if (split.Length >= 2)
            {
                var distriWithApostroph = split[1];
                var distri = distriWithApostroph.Replace("\"", string.Empty);
                return distri;
            }

            Client.Logger.Error(
                "Could not parse distribution. Make sure 'cat /etc/*-release' works on your distribution.");
            Client.Logger.Error($"Output of {DISTRIBUTION_CMD}: {Environment.NewLine}{output}");
            return N_A;
        }
    }
}