using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;

namespace Doods.Openmedivault.Ssh.Std.Requests
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