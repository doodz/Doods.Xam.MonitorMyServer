namespace Doods.Framework.Ssh.Std.Requests
{
    public class ReadFileRequest : SshRequestBase
    {
        public const string RequestString = "tail  -n 5 /var/log/syslog";

        public ReadFileRequest(string filepath, int lines = 5, bool useSudo = true) : base(
            $"tail -n {lines} {filepath}")
        {
            UseSudo = useSudo;
        }
    }
}