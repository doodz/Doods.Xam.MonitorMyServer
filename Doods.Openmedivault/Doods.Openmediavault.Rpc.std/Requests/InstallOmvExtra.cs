namespace Doods.Openmediavault.Rpc.Std.Requests
{
    public class InstallOmvExtra : OmvRequestBase
    {
        public const string RequestString =
            "wget -O - https://github.com/OpenMediaVault-Plugin-Developers/packages/raw/master/install | bash";

        public InstallOmvExtra() : base(RequestString)
        {
        }
    }
}