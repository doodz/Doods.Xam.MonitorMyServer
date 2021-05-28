using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;

namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
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