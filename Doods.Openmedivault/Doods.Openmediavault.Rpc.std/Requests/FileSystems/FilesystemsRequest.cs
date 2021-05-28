namespace Doods.Openmediavault.Rpc.Std.Requests.FileSystems
{
    public class FilesystemsRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc FileSystemMgmt enumerateFilesystems";

        public FilesystemsRequest() : base(RequestString)
        {
        }
    }
}