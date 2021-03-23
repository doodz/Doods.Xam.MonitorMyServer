namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetListFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt getListBg \"{\\\"start\\\":0,\\\"limit\\\":25}\"";

        public GetListFileSystemBackgroundRequest() : base(RequestString)
        {
        }
    }
}