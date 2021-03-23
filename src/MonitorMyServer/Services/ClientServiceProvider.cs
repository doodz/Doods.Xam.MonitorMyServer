// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="ClientServiceProvider.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/04/06 at 11:09: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using Doods.Synology.Webapi.Std;

namespace Doods.Xam.MonitorMyServer.Services
{
    public abstract class ClientServiceProvider<T>
    {
        //private volatile ISshService _value;//todo a voir
        public T Value { get; set; }

        public void ChangeValue(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Equals(Value)) return;

            // var oldValue = Value;
            Value = value;
            // oldValue.Dispose();
        }

        public void Dispose()
        {
            //this._value.Dispose();
        }
    }


    public class SshServiceProvider : ClientServiceProvider<ISshService>
    {
        public SshServiceProvider(SshService service)
        {
            Value = service;
        }
    }

    public class OmvServiceProvider : ClientServiceProvider<IOmvService>
    {
        public OmvServiceProvider()
        {
            Value = new OmvRpcService(null, null, null);
        }
    }

    public class WebminServiceProvider : ClientServiceProvider<IWebminCgiService>
    {
        public WebminServiceProvider()
        {
            //Value = new WebminCgiService(null, null, null);
        }
    }

    public class SynoServiceProvider : ClientServiceProvider<ISynologyCgiService>
    {
        public SynoServiceProvider()
        {
            //Value = new SynologyCgiService(null, null, null);
        }
    }
}