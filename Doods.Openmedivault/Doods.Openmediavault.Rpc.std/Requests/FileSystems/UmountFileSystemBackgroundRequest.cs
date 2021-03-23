using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;

namespace Doods.Openmedivault.Ssh.Std.Requests
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