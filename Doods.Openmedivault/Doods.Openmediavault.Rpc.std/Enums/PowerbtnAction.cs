using System.ComponentModel;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public enum PowerbtnAction
    {
        [Description("nothing")] Nothing,
        [Description("shutdown")] Shutdown,
        [Description("standby")] Standby
    }
}