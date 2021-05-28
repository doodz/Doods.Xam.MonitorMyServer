namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class GetListDickBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc DiskMgmt getListBg";

        public GetListDickBackgroundRequest() : base(RequestString)
        {
        }
    }
}