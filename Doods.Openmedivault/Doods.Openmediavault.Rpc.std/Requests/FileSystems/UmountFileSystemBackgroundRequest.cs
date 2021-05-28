using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;

namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class UmountFileSystemBackgroundRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt umount ";

        public UmountFileSystemBackgroundRequest(BaseOmvFilesystems filesystems) : base(RequestString +
            $"\"{{\\\"id\\\":\\\"{filesystems.Devicefile}\\\",\\\"fstab\\\":true}}\"")
        {
        }
    }
}