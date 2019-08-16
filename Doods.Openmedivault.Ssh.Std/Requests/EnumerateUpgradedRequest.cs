
namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class EnumerateUpgradedRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc apt enumerateUpgraded";
        public EnumerateUpgradedRequest() : base(RequestString)
        {
        }

    }
}