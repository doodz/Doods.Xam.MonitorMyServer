namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class UpdateAptListRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Apt update";

        public UpdateAptListRequest() : base(RequestString)
        {
        }
    }
}