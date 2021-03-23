namespace Doods.Synology.Webapi.Std
{
    public abstract class BaseSynoClient
    {
        protected readonly ISynoWebApi _client;
        protected string Resource = "/entry.cgi";
        protected string ServiceApiName;

        public BaseSynoClient(ISynoWebApi client)
        {
            _client = client;
        }
    }
}