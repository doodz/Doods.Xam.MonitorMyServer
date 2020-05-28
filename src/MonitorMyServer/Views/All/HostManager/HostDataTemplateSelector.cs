using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate SshTemplate { get; set; }
        public DataTemplate OmvTemplate { get; set; }
        public DataTemplate SynoTemplate { get; set; }
        public DataTemplate RpiTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            if (item is HostViewModel host)
            {
                if (host.IsOmvServer)
                {
                    return OmvTemplate;
                }
                if (host.IsRpi)
                {
                    return SshTemplate;
                }
                if (host.IsSynoServer)
                {
                    return SynoTemplate;
                }
                if (host.IsSsh)
                {
                    return SshTemplate;
                }

               
            }

            return DefaultTemplate;
        }
    }
}