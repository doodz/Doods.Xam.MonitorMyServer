using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;

namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class DeleteFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt delete ";

        public DeleteFileSystemBackgroundRequest(OmvFilesystems filesystem) : base(RequestString +
            $"\"{{\\\"id\\\":\\\"{filesystem.Uuid}\\\"}}\"")
        {
        }
    }
}