using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class NetworkInformationQuery : GenericQuery<IEnumerable<NetworkInterfaceInformationBean>>
    {
        public NetworkInformationQuery(IClientSsh client) : base(client)
        {
            CmdString = "ls -1 /sys/class/net";
        }

        private IEnumerable<NetworkInterfaceInformationBean> GetInterfeces(IEnumerable<string> interfaces)
        {

           var interfacesInfo = new List<NetworkInterfaceInformationBean>();
            // 1. find all network interfaces (excluding loopback interface) and
            // check carrier
            //var interfaces = new InterfaceQuery(Client).Run();
            //LOGGER.info("Available interfaces: {}", interfaces);

            foreach (var interfaceName in interfaces)
            {
                var interfaceInfo = new NetworkInterfaceInformationBean
                {
                    Name = interfaceName,
                    HasCarrier = CheckCarrier(interfaceName)
                };
                // check carrier
                interfacesInfo.Add(interfaceInfo);
            }
            var wirelessInterfaces = new List<NetworkInterfaceInformationBean>();
            // 2. for every interface with carrier check ip adress
            foreach (var interfaceBean in interfacesInfo)
            {
                if (interfaceBean.HasCarrier)
                {
                    interfaceBean.IpAdress = new IpAddressQuery(Client, interfaceBean.Name).Run();
                    // check if interface is wireless (interface name starts with "wlan")
                    if (interfaceBean.Name.StartsWith("wlan"))
                    {
                        // add to wireless interfaces list
                        wirelessInterfaces.Add(interfaceBean);
                    }
                }
            }
            // 3. query signal level and link status of wireless interfaces
            if (wirelessInterfaces.Any())
            {
                new WlanInfoQuery(Client, wirelessInterfaces).Run();
            }
            return interfacesInfo;
        }

        private bool CheckCarrier(string interfaceName)
        {
            return new CheckCarrierQuery(Client, interfaceName).Run();
        }
        protected override IEnumerable<NetworkInterfaceInformationBean> PaseResult(string result)
        {
           
            var res = result.Split('\n').Where(r => !r.StartsWith("lo") && !string.IsNullOrWhiteSpace(r) && !string.IsNullOrEmpty(r));
            return GetInterfeces(res);
        }
    }
}
