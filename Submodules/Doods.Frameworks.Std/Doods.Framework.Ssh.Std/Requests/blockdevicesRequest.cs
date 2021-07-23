namespace Doods.Framework.Ssh.Std.Requests
{
    public class BlockdevicesRequest : SshRequestBase
    {
        public const string RequestString = "lsblk -b -O -J";

        public BlockdevicesRequest() : base(RequestString)
        {
        }
    }
}