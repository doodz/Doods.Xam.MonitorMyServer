namespace Doods.Openmediavault.Rpc.Std.Requests
{
    public class IsDirtyRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Config isDisty";

        public IsDirtyRequest() : base(RequestString)
        {
        }
    }
}