// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="SynoAuthClientUnitTest.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/08/06 at 13:56: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Doods.Framework.Http.Std.Serializers;
using Doods.Synology.Webapi.Std;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Synology.Webapi.Std.NewFolder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Doods.Synology.TU.Datas
{
    public class LocalISynoWebApi : ISynoWebApi
    {
        private NewtonsoftJsonSerializer _serializer = new NewtonsoftJsonSerializer();
        public string ExtendJsonFileName = string.Empty;
        public Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            var result = new Mock<IRestResponse>();
            return Task.FromResult(result.Object);
        }

        public Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request)
        {
            var result = new Mock<IRestResponse<T>>();

            var b = request.Body;
            var o = (string)request.Parameters[0].Value;
            var oo = (string)request.Parameters[2].Value;
            var s = request.JsonSerializer;


            var toto = Directory.GetCurrentDirectory();
            var path = @$"{toto}/Data/Json/{o}.{oo}.json";
            if (!string.IsNullOrWhiteSpace(ExtendJsonFileName))
            {
                if (ExtendJsonFileName.Contains(@$"{o}.{oo}"))
                    path = @$"{toto}/Data/Json/{ExtendJsonFileName}.json";
            }

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

        public string Sid { get; set; }
        public DateTime LoggedInTime { get; set; }
        public IRestResponse Execute(IRestRequest request)
        {
            var result = new Mock<IRestResponse>();
            return result.Object;
        }

        public IDictionary<string, SynologyApiServicesInfo> ApiInfo { get; set; }
    }


    [TestClass]
    public class SynoAuthClientUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var client = new Mock<ISynoWebApi>();
            var v = new SynoAuthClient(client.Object);
            Assert.IsNotNull(v);

        }

        [TestMethod]
        public async Task LoginAsync_Error2()
        {
            var client = new LocalISynoWebApi();
            client.ExtendJsonFileName = "SYNO.API.Auth.login.Error2";
            var c = new SynoAuthClient(client);
            var result = await c.LoginAsync("toto", "tata");
            Assert.IsNotNull(result);
        }

    }
}