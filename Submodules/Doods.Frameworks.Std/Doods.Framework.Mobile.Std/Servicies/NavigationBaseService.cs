// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="NavigationBaseService.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2019
//  </copyright>
//  History:
//   2019/07/27 at 14:29:  aka therv.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Std.Servicies
{
    public abstract class NavigationBaseService
    {
        protected internal readonly ILogger _looger;
        protected internal readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        protected internal readonly object _sync = new object();
        protected internal readonly ITelemetryService _telemetry;

        public NavigationBaseService(ILogger looger, ITelemetryService telemetryService)
        {
            _looger = looger;
            _telemetry = telemetryService;
        }

        public Dictionary<string, Type> Routes => _routes;

        public string CurrentPageKey { get; }

        public virtual void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_routes.ContainsKey(pageKey))
                    _routes[pageKey] = pageType;
                else
                    _routes.Add(pageKey, pageType);
            }
        }
    }
}