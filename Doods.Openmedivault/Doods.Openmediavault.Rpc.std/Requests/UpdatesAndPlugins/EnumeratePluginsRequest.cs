namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class EnumeratePluginsRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Plugin enumeratePlugins";

        public EnumeratePluginsRequest() : base(RequestString)
        {
        }
    }
}