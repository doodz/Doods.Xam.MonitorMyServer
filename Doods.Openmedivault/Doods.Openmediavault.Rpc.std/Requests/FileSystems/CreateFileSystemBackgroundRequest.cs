using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class CreateFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt create ";

        public CreateFileSystemBackgroundRequest(BaseOmvFilesystems filesystems) : base(RequestString +
            $"\"{filesystems.ToJson(true)}\"")
        {
        }
    }
}