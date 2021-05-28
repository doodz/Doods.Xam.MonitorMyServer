namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class EnumeratePluginsRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Plugin enumeratePlugins";

        public EnumeratePluginsRequest() : base(RequestString)
        {
        }
    }
}