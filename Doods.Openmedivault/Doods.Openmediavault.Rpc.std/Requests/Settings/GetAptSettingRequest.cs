namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class GetAptSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc Apt getSettings";

        public GetAptSettingRequest() : base(_commandText)
        {
        }
    }
}