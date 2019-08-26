namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetPlugins : OmvRequestBase
    {

        public const string RequestString = "omv-rpc Plugin enumeratePlugins";
        public GetPlugins() : base(RequestString)
        {
        }

    }
}