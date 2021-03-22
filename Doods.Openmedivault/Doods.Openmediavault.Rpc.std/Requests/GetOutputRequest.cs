namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetOutputRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Exec isRunning  \"{\\\"filename\\\":\\\"0\\\"}\"";

        public GetOutputRequest(string filename, long pos) : base(
            $"omv-rpc Exec getOutput  \"{{\\\"filename\\\":\\\"{filename}\\\",\\\"pos\\\":{pos}}}\"")
        {
        }
    }
}