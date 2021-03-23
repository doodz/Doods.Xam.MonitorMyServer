namespace Doods.Webmin.Webapi.Std.Clients
{
    public abstract class BaseWebminClient
    {
        protected readonly IWebminApi _client;
        protected string Resource = "/entry.cgi";
        protected string ServiceApiName;

        public BaseWebminClient(IWebminApi client)
        {
            _client = client;
        }
    }
}