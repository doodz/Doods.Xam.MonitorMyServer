using System.Runtime.InteropServices;

namespace Doods.Framework.Std.Helpers
{
    public static class PlatformHelpers
    {
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static bool IsOsx()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }
}