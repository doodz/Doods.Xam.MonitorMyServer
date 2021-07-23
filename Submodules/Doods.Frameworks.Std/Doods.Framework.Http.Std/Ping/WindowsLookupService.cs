using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Doods.Framework.Http.Std.Extensions;
using Doods.Framework.Std.Helpers;

namespace Doods.Framework.Http.Std.Ping
{
    internal static class WindowsLookupService
    {
        public static bool IsSupported => PlatformHelpers.IsWindows();

        // based on https://github.com/nikeee/wake-on-lan/blob/5bdcecc/src/WakeOnLan/ArpRequest.cs
        /// <summary>
        ///     Call ApHlpApi.SendARP to lookup the mac address on windows-based systems.
        /// </summary>
        /// <exception cref="Win32Exception">If IpHlpApi.SendARP returns non-zero.</exception>
        public static PhysicalAddress Lookup(IPAddress ip)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException();
            if (ip == null)
                throw new ArgumentNullException(nameof(ip));

            var destIp = BitConverter.ToInt32(ip.GetAddressBytes(), 0);

            var addr = new byte[6];
            var len = addr.Length;

            var res = NativeMethods.SendARP(destIp, 0, addr, ref len);

            if (res == 0)
                return new PhysicalAddress(addr);
            throw new Win32Exception(res);
        }

        // based on https://github.com/nikeee/wake-on-lan/blob/4dfa0fd/src/WakeOnLan/NativeMethods.cs
        private static class NativeMethods
        {
            private const string IphlpApi = "iphlpapi.dll";

            [DllImport(IphlpApi, ExactSpelling = true)]
            [SecurityCritical]
            internal static extern int SendARP(int destinationIp, int sourceIp, byte[] macAddress,
                ref int physicalAddrLength);
        }
    }

    internal static class LinuxLookupService
    {
        private const string ArpTablePath = "/proc/net/arp";

        private static readonly Regex lineRegex =
            new Regex(@"^((?:[0-9]{1,3}\.){3}[0-9]{1,3})(?:\s+\w+){2}\s+((?:[0-9A-Fa-f]{2}[:-]){5}(?:[0-9A-Fa-f]{2}))");

        public static bool IsSupported => PlatformHelpers.IsLinux() && File.Exists(ArpTablePath);

        public static async Task<PhysicalAddress> PingThenTryReadFromArpTable(IPAddress ip, TimeSpan timeout)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException();
            using (var ping = new System.Net.NetworkInformation.Ping())
            {
                var reply = await ping.SendPingAsync(ip, (int) timeout.TotalMilliseconds).ConfigureAwait(false);
                return await TryReadFromArpTable(ip).ConfigureAwait(false);
            }
        }

        public static async Task<PhysicalAddress> TryReadFromArpTable(IPAddress ip)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException();
            using (var arpFile = new FileStream(ArpTablePath, FileMode.Open, FileAccess.Read))
            {
                return await ParseProcNetArp(arpFile, ip).ConfigureAwait(false);
            }
        }

        private static async Task<PhysicalAddress> ParseProcNetArp(Stream content, IPAddress ip)
        {
            using (var reader = new StreamReader(content))
            {
                await reader.ReadLineAsync().ConfigureAwait(false); // first line is header, skip
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (string.IsNullOrWhiteSpace(line))
                        return null;
                    try
                    {
                        var mac = ParseIfMatch(line, ip);
                        if (mac != null)
                            return mac;
                    }
                    catch (FormatException)
                    {
                        throw new PlatformNotSupportedException();
                        ;
                    }
                }
            }

            return null;
        }

        private static PhysicalAddress ParseIfMatch(string line, IPAddress ip)
        {
            var m = lineRegex.Match(line);
            if (!m.Success || m.Groups.Count != 3)
                throw new FormatException($"The given line '{line}' was not in the expected /proc/net/arp format.");
            var tableIpStr = m.Groups[1].Value;
            var tableMacStr = m.Groups[2].Value;
            var tableIp = IPAddress.Parse(tableIpStr);
            if (!tableIp.Equals(ip))
                return null;
            return tableMacStr.ParseMacAddress();
        }
    }
}