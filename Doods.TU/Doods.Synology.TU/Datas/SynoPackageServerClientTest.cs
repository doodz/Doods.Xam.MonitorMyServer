using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Doods.Framework.Http.Std.Serializers;
using Doods.Synology.Webapi.Std;
using Doods.Synology.Webapi.Std.Datas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Doods.Synology.TU.Datas
{
    [TestClass]
    public class SynoPackageServerClientTest
    {
        [TestMethod]
        public void Create()
        {
            var client = new Mock<ISynoWebApi>();
            var v = new SynoPackageServerClient(client.Object);
            Assert.IsNotNull(v);
           
        }
        [TestMethod]
        public async Task ServiceApiName()
        {

            var client = new Mock<ISynoWebApi>();



            client.Setup(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.IsAny<IRestRequest>()))
                .ReturnsAsync(new RestResponse<SynologyResponse<SynologyPackageServer>>()
                {
                    Data = new SynologyResponse<SynologyPackageServer>() { Data = new SynologyPackageServer() }
                });

            var c = new SynoPackageServerClient(client.Object);
            var result = await c.GetPackagesServerInfo();

            client.Verify(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.Is<SynologyRestRequest>(arg =>
                arg.Parameters.Any(p => p.Value != null && p.Value.ToString() == "SYNO.Core.Package.Server"))));



        }
        [TestMethod]
        public async Task Resource()
        {
            var client = new Mock<ISynoWebApi>();

            client.Setup(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.IsAny<IRestRequest>()))
                .ReturnsAsync(new RestResponse<SynologyResponse<SynologyPackageServer>>()
                {
                    Data = new SynologyResponse<SynologyPackageServer>() { Data = new SynologyPackageServer() }
                });

            var c = new SynoPackageServerClient(client.Object);
            var result = await c.GetPackagesServerInfo();

            client.Verify(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.Is<SynologyRestRequest>(arg =>
                arg.Resource == "/entry.cgi")));
        }
        [TestMethod]
        public async Task GetPackagesServerInfo()
        {
            var client = new Mock<ISynoWebApi>();
 
            client.Setup(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.IsAny<IRestRequest>()))
                .ReturnsAsync(new RestResponse<SynologyResponse<SynologyPackageServer>>()
                {
                    Data = new SynologyResponse<SynologyPackageServer>(){Data = new SynologyPackageServer()}
                });

            var c = new SynoPackageServerClient(client.Object);
            var result =await c.GetPackagesServerInfo();
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public async Task GetPackagesServerInfoJson()
        //{
        //    var client = new Mock<ISynoWebApi>();
        //    var toto = Directory.GetCurrentDirectory();
        //    client.Setup(c => c.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(It.IsAny<IRestRequest>()))
        //        .ReturnsAsync(new RestResponse<SynologyResponse<SynologyPackageServer>>()
        //        {
        //            Data = GetResult<SynologyResponse<SynologyPackageServer>>(@$"{toto}/Data/Json/SYNO.Core.Package.Server_list.json")
        //        });

        //    var c = new SynoPackageServerClient(client.Object);
        //    var result = await c.GetPackagesServerInfo();
        //    Assert.IsNotNull(result);
        //}

        private T GetResult<T>(string jsonFile)
        {
                var serialiser = new NewtonsoftJsonSerializer();
                return serialiser.Deserialize<T>(File.ReadAllText(jsonFile));
           
        }


    }
}