using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;

namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class MountFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt mount ";

        public MountFileSystemBackgroundRequest(BaseOmvFilesystems filesystems) : base(RequestString +
            $"\"{{\\\"id\\\":\\\"{filesystems.Devicefile}\\\",\\\"fstab\\\":true}}\"")
        {
        }
    }
}