namespace Doods.Synology.Webapi.Std
{
    public abstract class BaseSynoClient
    {
        protected readonly ISynoWebApi _client;
        protected string ServiceApiName;
        protected string Resource = "/entry.cgi";
        public BaseSynoClient(ISynoWebApi client)
        {
            _client = client;
        }
    }
}