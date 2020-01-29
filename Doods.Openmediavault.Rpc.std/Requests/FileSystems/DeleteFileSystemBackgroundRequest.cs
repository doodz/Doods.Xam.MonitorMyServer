using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
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