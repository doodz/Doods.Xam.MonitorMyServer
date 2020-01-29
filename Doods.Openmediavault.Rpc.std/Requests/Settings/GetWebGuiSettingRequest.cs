namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetWebGuiSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc WebGui getSettings";

        public GetWebGuiSettingRequest() : base(_commandText)
        {
        }
    }
}