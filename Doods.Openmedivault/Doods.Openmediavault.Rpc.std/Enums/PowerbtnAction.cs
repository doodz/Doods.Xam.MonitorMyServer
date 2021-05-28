using System.ComponentModel;

namespace Doods.Openmediavault.Rpc.Std.Enums
{
    public enum PowerbtnAction
    {
        [Description("nothing")] Nothing,
        [Description("shutdown")] Shutdown,
        [Description("standby")] Standby
    }
}