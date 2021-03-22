namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetOmvVersion : OmvRequestBase
    {
        public const string RequestString =
            "dpkg -l openmediavault | awk '$2 == \"openmediavault\" { print substr($3,1,1) }'";

        public GetOmvVersion() : base(RequestString)
        {
        }
    }
}