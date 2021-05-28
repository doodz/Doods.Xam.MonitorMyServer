namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class GetDateAndTimeSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc System getTimeSettings";

        public GetDateAndTimeSettingRequest() : base(_commandText)
        {
        }
    }
}