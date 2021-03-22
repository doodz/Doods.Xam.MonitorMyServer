using Doods.Openmediavault.Rpc.std.Data.V4.Settings;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetDateAndTimeSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc System setTimeSettings ";

        public SetDateAndTimeSettingRequest(TimeSetting timeSetting) : base(_commandText +
                                                                            $"\"{timeSetting.ToJson(true)}\"")
        {
        }
    }
}