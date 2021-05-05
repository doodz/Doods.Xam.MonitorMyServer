using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Http.Std.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Doods.Framework.TU.Authentication
{
    [TestClass]
    public class SimpleHttpHeaderAuthenticatorUnitTest
    {
        [TestMethod]
        public void Create()
        {
            string username = "foo";
            string password = "bar";
            var obj = new SimpleHttpHeaderAuthenticator(username, password);
            Assert.IsNotNull(obj);
        }


        [TestMethod]
        public void Authenticate()
        {
            string username = "foo";
            string password = "bar";

            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();

            requestMock.Setup(r => r.AddHeader(It.IsAny<string>(), It.IsAny<string>()));

            var obj = new SimpleHttpHeaderAuthenticator(username, password);
            Assert.IsNotNull(obj);
            obj.Authenticate(clientMock.Object, requestMock.Object);
            requestMock.Verify(r=>r.AddHeader(It.IsAny<string>(), It.IsAny<string>()),Times.Once);
            requestMock.Verify(r => r.AddHeader(It.Is<string>(u=> u==username), It.Is<string>(p=>p ==password)), Times.Once);
        }
    }
}
