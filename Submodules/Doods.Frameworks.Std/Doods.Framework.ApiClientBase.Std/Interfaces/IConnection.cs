using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Models;

namespace Doods.Framework.ApiClientBase.Std.Interfaces
{
    public interface IConnection
    {
        int Port { get; }
        string Host { get; }

        Credentials Credentials { get; }
        ConnectionType ConnectionType { get; }
    }
}