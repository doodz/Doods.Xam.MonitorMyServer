﻿// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="AptSettingViewModelUnitTest.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/08/05 at 16:16: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.TU;
using Doods.Openmedivault.Http.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.OpenMediaVault.OpenmediavaultSettings
{
    [TestClass]
    public class AptSettingViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var synologyCgiService = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new AptSettingViewModel();
            Assert.IsNotNull(obj);


        }


        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var logger = new Mock<ILogger>();
            var mapper = new Mock<IMapper>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();

            logger.SetupAllProperties();
            mapper.SetupAllProperties();
            configurationMock.SetupAllProperties();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            var obj = new AptSettingViewModel();

            Assert.IsNull(obj.Title);
            await obj.OnAppearingAsync();

        }
    }
}