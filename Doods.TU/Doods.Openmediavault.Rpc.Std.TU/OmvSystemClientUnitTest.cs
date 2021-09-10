using System.IO;
using System.Threading.Tasks;
using Doods.Framework.Http.Std;
using Doods.Framework.Http.Std.Serializers;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmedivault.Http.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace Doods.Openmediavault.Rpc.Std.TU
{


    public class LocalIHttpClient : IHttpClient
    {
        internal  class Params
        {
            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("pos")]
            public long Pos { get; set; }
        }
        public string OmvV = "V6";
        private NewtonsoftJsonSerializer _serializer = new NewtonsoftJsonSerializer();
        public Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            var result = new Mock<IRestResponse>();



            return Task.FromResult(result.Object);
        }

        public Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request)
        {
            
            var result = new Mock<IRestResponse<T>>();

            var b = request.Body;
            var o = (Doods.Openmediavault.Rpc.Std.RpcRequest)request.Parameters[0].Value;

            var s =request.JsonSerializer;


            var toto = Directory.GetCurrentDirectory();
            var path = @$"{toto}/Data/{OmvV}/Error.json";
            if (o.Method == "getOutput")
            {
                System.Type type = o.Params.GetType();
                string filename = (string)type.GetProperty("filename").GetValue(o.Params, null);
                if (!string.IsNullOrWhiteSpace(filename))
                {
                    path = @$"{toto}/Data/{OmvV}/{filename}.json";
                }
            }
            else
                path =@$"{toto}/Data/{OmvV}/{o.Service}_{o.Method}.json";

            //if (!File.Exists(path))
            //{
            //    path = @$"{toto}/Data/V6/Error.json";
            //}

            string jsonString = File.ReadAllText(path);

            result.Setup(c => c.Data).Returns(() =>
            {
               var obj = _serializer.Deserialize<T>(jsonString);
               return obj;
            });


            return Task.FromResult(result.Object);
        }
    }


    [TestClass]
    public class OmvSystemClientUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var client = new Mock<IHttpClient>(); 
            var rpc = new OmvHttpService(logger.Object, client.Object);
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
        }
        [TestMethod]
        public void GetRpcVersion()
        {
            var logger = new Mock<ILogger>();
           
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
            var v =systemClient.GetRpcVersion();
            Assert.IsNotNull(v);
            Assert.AreEqual(OMVVersions.Version6, v);
        }
        [TestMethod]
        public async Task CheckRpcVersionAsync()
        {
            var logger = new Mock<ILogger>();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
            var v = await systemClient.CheckRpcVersionAsync();
            Assert.IsNotNull(v);
            Assert.AreEqual(OMVVersions.Version6, v);
        }

        [TestMethod]
        public async Task GetSystemInformations()
        {
            var logger = new Mock<ILogger>();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
            var obj = await systemClient.GetSystemInformations();

            Assert.IsFalse(obj.ConfigDirty);
            Assert.AreEqual("Intel(R) Celeron(R) CPU J3455 @ 1.50GHz", obj.CpuModelName);
            Assert.AreEqual(0.9900990099009901d, obj.CpuUsage);
            Assert.AreEqual("openmediavault.local", obj.Hostname);
            Assert.AreEqual("Linux 5.10.0-7-amd64", obj.Kernel);
            Assert.IsFalse(obj.LegacyMode);
            //Assert.AreEqual("", obj.LoadAverage);
            Assert.AreEqual(1023819776L, obj.MemTotal);
            Assert.AreEqual(120344576L, obj.MemUsed);
            Assert.IsTrue( obj.PkgUpdatesAvailable);
            Assert.IsFalse( obj.RebootRequired);
            Assert.AreEqual("6.0-16 (Shaitan)", obj.Version);
            //Assert.AreEqual("", obj.Uptime);
            Assert.AreEqual(1628067143L, obj.Ts);
            Assert.AreEqual("Wed 04 Aug 2021 10:52:23 AM CEST", obj.Time);

        }

        [TestMethod]
        public async Task GetTimeZoneList()
        {
            var logger = new Mock<ILogger>();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
            var obj = await systemClient.GetTimeZoneList();
            Assert.IsNotNull(obj);



        }

        [TestMethod]
        public async Task GetDateAndTimeSetting()
        {
            var logger = new Mock<ILogger>();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var systemClient = new OmvSystemClient(rpc);
            Assert.IsNotNull(systemClient);
            var obj = await systemClient.GetDateAndTimeSetting();
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Ntpenable);
            Assert.AreEqual("", obj.Timezone);
            Assert.AreEqual("pool.ntp.org", obj.Ntptimeservers);
        }
    }
}