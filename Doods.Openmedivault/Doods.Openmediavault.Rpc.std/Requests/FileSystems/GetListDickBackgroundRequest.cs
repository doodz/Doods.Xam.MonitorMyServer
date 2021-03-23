namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetListDickBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc DiskMgmt getListBg";

        public GetListDickBackgroundRequest() : base(RequestString)
        {
        }
    }
}