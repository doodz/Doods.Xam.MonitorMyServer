using System.ComponentModel;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public enum TimePickerEnum
    {
        [Description("Nothing")]
        Nothing,
        [Description("1Minute")]
        Minute1,
        [Description("2Minutes")]
        Minutes2,
        [Description("3Minutes")]
        Minutes3,
        [Description("4Minutes")]
        Minutes4,
        [Description("5Minutes")]
        Minutes5,
        [Description("10Minutes")]
        Minutes10,
        [Description("15Minutes")]
        Minutes15,
        [Description("30Minutes")]
        Minutes30,
        [Description("60Minutes")]
        Minutes60,
        [Description("1Day")]
        Day1
    }
}