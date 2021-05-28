namespace Doods.Openmediavault.Rpc.Std.Requests
{
    public class GetTimeZoneListRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc System getTimeZoneList";

        public GetTimeZoneListRequest() : base(_commandText)
        {
        }
    }
}