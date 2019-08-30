using Doods.Openmedivault.Ssh.Std.Data;

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