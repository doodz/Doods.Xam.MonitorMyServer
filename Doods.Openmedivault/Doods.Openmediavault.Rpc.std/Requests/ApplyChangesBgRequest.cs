namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class ApplyChangesBgRequest : OmvRequestBase
    {
        public ApplyChangesBgRequest() : base("omv-rpc Config applyChangesBg " +
                                              @"""{\""modules\"":[],\""force\"":false}""")
        {
        }
    }
}