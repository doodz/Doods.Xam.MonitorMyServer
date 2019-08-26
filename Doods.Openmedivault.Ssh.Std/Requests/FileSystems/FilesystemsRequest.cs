using Doods.Framework.Ssh.Std.Requests;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class FilesystemsRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc FileSystemMgmt enumerateFilesystems";
        public FilesystemsRequest() : base(RequestString)
        {
        }

    }
}