namespace Doods.Synology.Webapi.Std
{
    class BaseSynoClient
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