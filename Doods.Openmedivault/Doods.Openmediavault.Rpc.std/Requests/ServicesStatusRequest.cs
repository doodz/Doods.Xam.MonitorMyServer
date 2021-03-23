namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class ServicesStatusRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Services getStatus";

        public ServicesStatusRequest() : base(RequestString)
        {
        }
    }
}