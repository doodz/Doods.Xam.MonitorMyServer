using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetAptSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc Apt setSettings ";

        public SetAptSettingRequest(AptSetting aptSetting) : base(_commandText + $"\"{aptSetting.ToJson(true)}\"")
        {
        }
    }
}