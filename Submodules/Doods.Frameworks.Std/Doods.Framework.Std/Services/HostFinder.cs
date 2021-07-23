using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Doods.Framework.Std.Services
{
    /// <summary>Provides data for the <see cref="E:HostFinderCompleted"></see> event.</summary>
    public class HostFinderCompletedEventArgs : AsyncCompletedEventArgs
    {
        internal HostFinderCompletedEventArgs(PingReply reply, Exception error, bool cancelled, object userState) :
            base(error, cancelled, userState)
        {
            Reply = reply;
        }

        /// <summary>
        ///     Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol
        ///     (ICMP) echo request message and receive a corresponding ICMP echo reply message.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Net.NetworkInformation.PingReply"></see> object that describes the results of the ICMP
        ///     echo request.
        /// </returns>
        public PingReply Reply { get; }
    }

    /// <summary>
    ///     Represents the method that will handle the <see cref="E:HostFinderCompleted"></see> event of a
    ///     <see cref="T:HostFinder"></see> object.
    /// </summary>
    /// <param name="sender">The source of the <see cref="E:HostFinderCompleted"></see> event.</param>
    /// <param name="e">
    ///     A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs"></see> object that contains the
    ///     event data.
    /// </param>
    public delegate void HostFinderCompletedEventHandler(object sender, PingCompletedEventArgs e);

    public class Host
    {
        public string Ip { get; set; }
        public string Name { get; set; }
        public long RoundtripTime { get; set; }
    }

    public class HostFinder : NotifyPropertyChangedBase
    {
        private readonly ILogger _logger;
        private int _upCount;

        public HostFinder(ILogger logger)
        {
            _logger = logger;
        }

        public List<Host> ReachableAdress { get; } = new List<Host>();

        /// <summary>Occurs when process is completed</summary>
        /// <returns></returns>
        public event HostFinderCompletedEventHandler HostFinderCompleted;

        public void SearchHosts()
        {
            _upCount = 0;
            ReachableAdress.Clear();
            //var hostName = Dns.GetHostName();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = null;
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    localIP = ip.ToString();

            var ipBase = localIP;
            var ipParts = ipBase.Split('.');

            ipBase = ipParts[0] + "." + ipParts[1] + "." + ipParts[2] + ".";
            for (var i = 1; i < 254; i++)
            {
                var ip = ipBase + i;
                if (localIP == ip) continue;
                var p = new Ping();
                p.PingCompleted += p_PingCompleted;
                p.SendAsync(ip, 100, ip);
            }
        }

        private void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var ip = (string) e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                string name;
                if (true)
                {
                    try
                    {
                        var hostEntry = Dns.GetHostEntry(ip);
                        name = hostEntry.HostName;
                    }
                    catch (SocketException)
                    {
                        name = "?";
                    }

                    _logger.Info($"{ip} ({name}) is up: ({e.Reply.RoundtripTime} ms)");
                }
                else
                {
                    _logger.Info($"{ip} is up: ({e.Reply.RoundtripTime} ms)");
                }

                Interlocked.Increment(ref _upCount);
                ReachableAdress.Add(new Host {Ip = ip, Name = name, RoundtripTime = e.Reply.RoundtripTime});
            }
            else if (e.Reply == null)
            {
                _logger.Info($"Pinging {ip} failed. (Null Reply object?)");
            }

            var notify = _upCount % 10 == 0;
            if (notify || ip.EndsWith("254")) OnPropertyChanged(nameof(ReachableAdress));
        }
    }
}