using Doods.Xam.MonitorMyServer.Enums;

namespace Doods.Xam.MonitorMyServer.Views.SelectService
{
    public class ServiceDescription
    {
        public SupportedServicies Type { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }

        public string Description { get; set; }
    }
}