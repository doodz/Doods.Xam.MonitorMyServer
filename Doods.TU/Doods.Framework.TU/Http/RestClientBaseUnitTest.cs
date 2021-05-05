using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Http.Std;
using Doods.Framework.Http.Std.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Doods.Framework.TU.Http
{
    internal class SimpleRestClient : RestClientBase
    {
        public SimpleRestClient(IConnection connection) : base(connection)
        {
        }

        public SimpleRestClient(IConnection connection, NewtonsoftJsonSerializer serializer) : base(connection, serializer)
        {
        }

        protected override void AddHeaders(IRestRequest request)
        {
          
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }
    }


    [TestClass]
    public class RestClientBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var mockConnection = new Mock<IConnection>();

            mockConnection.Setup(connection => connection.Host).Returns("http://gbdtc");
            mockConnection.Setup(connection => connection.Port).Returns(42);
            mockConnection.Setup(connection => connection.ConnectionType).Returns(ConnectionType.Http);
            mockConnection.Setup(connection => connection.Credentials).Returns(new Credentials());
            var obj = new SimpleRestClient(mockConnection.Object);
            Assert.IsNotNull(obj);
            //var rsc = "toto";
            //var mth = RestSharp.Method.GET;
            //var obj = new HttpRequest(rsc, mth);
            //Assert.IsNotNull(obj);
            //Assert.AreEqual(rsc, obj.Resource);
            //Assert.AreEqual(mth, obj.Method);
            //Assert.IsInstanceOfType(obj.JsonSerializer, typeof(Doods.Framework.Http.Std.Serializers.NewtonsoftJsonSerializer));

        }
        [TestMethod]
        public void Create2()
        {
            var mockConnection = new Mock<IConnection>();
            mockConnection.Setup(connection => connection.Host).Returns("https://gbdtc");
            mockConnection.Setup(connection => connection.Port).Returns(42);
            mockConnection.Setup(connection => connection.ConnectionType).Returns(ConnectionType.Http);
            mockConnection.Setup(connection => connection.Credentials).Returns(new Credentials());
            var slz = new Doods.Framework.Http.Std.Serializers.NewtonsoftJsonSerializer();
            var obj = new SimpleRestClient(mockConnection.Object, slz);
            Assert.IsNotNull(obj);

            //var rsc = "toto";
            //var mth = RestSharp.Method.GET;
            //var obj = new HttpRequest(rsc, mth);
            //Assert.IsNotNull(obj);
            //Assert.AreEqual(rsc, obj.Resource);
            //Assert.AreEqual(mth, obj.Method);
            //Assert.IsInstanceOfType(obj.JsonSerializer, typeof(Doods.Framework.Http.Std.Serializers.NewtonsoftJsonSerializer));

        }
    }
}