namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetListCandidatesFileSystemBackgroundRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc FileSystemMgmt getCandidatesBg";
        public GetListCandidatesFileSystemBackgroundRequest() : base(RequestString)
        {
        }

    }
}