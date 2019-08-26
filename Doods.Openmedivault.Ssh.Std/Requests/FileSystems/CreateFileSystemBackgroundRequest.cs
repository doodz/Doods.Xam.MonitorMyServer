using Doods.Openmedivault.Ssh.Std.Data;

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