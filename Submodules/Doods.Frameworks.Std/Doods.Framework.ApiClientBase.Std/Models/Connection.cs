using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Interfaces;

namespace Doods.Framework.ApiClientBase.Std.Models
{
    public abstract class ConnectionBase : IConnection
    {
        protected ConnectionBase(string host, int port, Credentials credentials)
        {
            Host = host;
            Port = port;
            Credentials = credentials;
        }

        /// <summary>Gets connection port.</summary>
        public int Port { get; }

        /// <summary>Gets connection host.</summary>
        public string Host { get; }

        public Credentials Credentials { get; }
        public ConnectionType ConnectionType { get; protected set; }
    }
}