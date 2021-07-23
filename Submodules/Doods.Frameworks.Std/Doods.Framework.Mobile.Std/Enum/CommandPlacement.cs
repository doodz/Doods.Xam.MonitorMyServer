using System;

namespace Doods.Framework.Mobile.Std.Enum
{
    [Flags]
    public enum CommandPlacement
    {
        ContextMenu = 1,
        Card = 2,
        All = ContextMenu | Card
    }
}