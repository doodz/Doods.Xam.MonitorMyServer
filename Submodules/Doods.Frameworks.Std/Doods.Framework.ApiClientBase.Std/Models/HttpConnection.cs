using Doods.Framework.ApiClientBase.Std.Authentication;

namespace Doods.Framework.ApiClientBase.Std.Models
{
    public class HttpConnection : ConnectionBase
    {
        public HttpConnection(string host, int port) : base(host, port, new Credentials())
        {
            ConnectionType = ConnectionType.Http;
        }
    }
}