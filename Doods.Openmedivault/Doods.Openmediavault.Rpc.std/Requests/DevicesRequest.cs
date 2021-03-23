namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class DevicesRequest : OmvRequestBase
    {
        public const string RequestString =
            "omv-rpc Network enumerateDevicesList \"{\\\"start\\\":0,\\\"limit\\\":25}\"";

        public DevicesRequest() : base(RequestString)
        {
        }
    }
}