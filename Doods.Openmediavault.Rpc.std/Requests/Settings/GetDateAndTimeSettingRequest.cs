namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetDateAndTimeSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc System getTimeSettings";

        public GetDateAndTimeSettingRequest() : base(_commandText)
        {
        }
    }
}