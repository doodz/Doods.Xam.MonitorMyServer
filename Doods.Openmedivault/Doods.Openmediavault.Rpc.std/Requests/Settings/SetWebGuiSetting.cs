using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetWebGuiSetting : OmvRequestBase
    {
        private static string _commandText = "omv-rpc WebGui setSettings ";

        public SetWebGuiSetting(WebGuiSetting webGuiSetting) : base(_commandText + $"\"{webGuiSetting.ToJson(true)}\"")
        {
            
        }
    }
}