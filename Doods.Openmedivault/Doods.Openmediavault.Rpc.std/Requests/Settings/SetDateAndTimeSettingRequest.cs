using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
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