namespace Doods.Openmediavault.Rpc.Std.Requests
{
    public class GenerateRddRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Rrd generate";

        public GenerateRddRequest() : base(RequestString)
        {
        }
    }
}