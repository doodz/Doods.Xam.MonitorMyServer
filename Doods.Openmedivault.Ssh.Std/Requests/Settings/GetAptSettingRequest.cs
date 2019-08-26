namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetAptSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc Apt getSettings";

        public GetAptSettingRequest() : base(_commandText)
        {
        }
    }
}