using System;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime d)
        {
            var epoch = d - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long) epoch.TotalSeconds;
        }
    }

    public class SetDateSettingsRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc System setDate ";

        public SetDateSettingsRequest(DateTime date) : base(_commandText +
                                                            $"\"{{\"timestamp\":{date.ToUnixTimestamp()}}}\"")
        {
        }
    }
}