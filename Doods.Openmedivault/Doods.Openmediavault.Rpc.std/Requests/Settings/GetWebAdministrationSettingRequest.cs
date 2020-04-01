namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetWebAdministrationSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc WebGui getSettings ";

        public GetWebAdministrationSettingRequest() : base(_commandText)
        {
        }
    }
}