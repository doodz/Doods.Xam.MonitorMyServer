namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class IsRunningRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Exec isRunning  \"{\\\"filename\\\":\\\"0\\\"}\"";

        public IsRunningRequest(string filename) : base(
            $"omv-rpc Exec isRunning  \"{{\\\"filename\\\":\\\"{filename}\\\"}}\"")
        {
        }
    }
}