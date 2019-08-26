using System;
using Doods.Framework.Std.Extensions;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetDateSettingsRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc System setDate ";

        public SetDateSettingsRequest(DateTime date) : base(_commandText + $"\"{{\"timestamp\":{date.ToUnixTimestamp()}}}\"")
        {
        }
    }
}