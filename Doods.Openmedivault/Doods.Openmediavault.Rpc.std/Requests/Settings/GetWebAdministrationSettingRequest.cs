namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetWebAdministrationSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc WebGui getSettings ";

        public GetWebAdministrationSettingRequest() : base(_commandText)
        {
        }
    }
}