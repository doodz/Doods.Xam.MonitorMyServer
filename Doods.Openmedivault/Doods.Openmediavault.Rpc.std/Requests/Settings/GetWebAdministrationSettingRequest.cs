namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class GetWebAdministrationSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc WebGui getSettings ";

        public GetWebAdministrationSettingRequest() : base(_commandText)
        {
        }
    }
}