namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class GetListCandidatesFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt getCandidatesBg";

        public GetListCandidatesFileSystemBackgroundRequest() : base(RequestString)
        {
        }
    }
}