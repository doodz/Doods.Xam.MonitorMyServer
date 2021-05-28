using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class SetAptSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc Apt setSettings ";

        public SetAptSettingRequest(AptSetting aptSetting) : base(_commandText + $"\"{aptSetting.ToJson(true)}\"")
        {
        }
    }
}