namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SystemInformationRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc System getInformation";

        public SystemInformationRequest() : base(RequestString)
        {
        }
    }
}