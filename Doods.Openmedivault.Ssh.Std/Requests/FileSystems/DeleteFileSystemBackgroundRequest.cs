namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class DeleteFileSystemBackgroundRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc FileSystemMgmt create";
        public DeleteFileSystemBackgroundRequest(string id) : base(RequestString +
                                                                   $"\"{{\\\"id\\\":\\\"{id}\\\"}}\"")
        {


        }

    }
}