using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class SetWebGuiSetting : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc WebGui setSettings ";

        public SetWebGuiSetting(WebGuiSetting webGuiSetting) : base(_commandText + $"\"{webGuiSetting.ToJson(true)}\"")
        {
        }
    }
}