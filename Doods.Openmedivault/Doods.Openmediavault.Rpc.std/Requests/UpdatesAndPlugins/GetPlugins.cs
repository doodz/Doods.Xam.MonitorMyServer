namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class GetPlugins : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Plugin enumeratePlugins";

        public GetPlugins() : base(RequestString)
        {
        }
    }
}