using System;
using System.Globalization;
using System.Net.NetworkInformation;

namespace Doods.Framework.Http.Std.Extensions
{
    internal static class StringExtensions
    {
        public static PhysicalAddress ParseMacAddress(this string mac)
        {
            var macString = mac?.Replace(":", "-")?.ToUpper(CultureInfo.InvariantCulture) ??
                            throw new ArgumentNullException(nameof(mac));
            return PhysicalAddress.Parse(macString);
        }
    }
}