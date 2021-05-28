namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class EnumerateUpgradedRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc apt enumerateUpgraded";

        public EnumerateUpgradedRequest() : base(RequestString)
        {
        }
    }
}