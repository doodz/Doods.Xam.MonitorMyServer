using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetDateAndTimeSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc System setTimeSettings ";

        public SetDateAndTimeSettingRequest(TimeSetting timeSetting) : base(_commandText + $"\"{timeSetting.ToJson(true)}\"")
        {
        }
    }
}