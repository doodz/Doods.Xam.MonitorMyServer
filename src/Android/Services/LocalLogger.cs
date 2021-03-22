// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="LocalLogger.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2020
//  </copyright>
//  History:
//   2020/05/28 at 15:17: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using System;
using Android.Util;

namespace Doods.Xam.MonitorMyServer.Droid.Services
{
    internal class LocalLogger
    {
        private readonly string _tag;

        public LocalLogger(string defaultTag = null)
        {
            _tag = defaultTag ?? "MonitorMyServer";
        }

        public void Debug(string msg)
        {
            Debug(_tag, msg);
        }

        public void Debug(string tag, string msg)
        {
            InternalLog(tag, msg, (t, m) => Log.Info(t ?? _tag, m));
        }

        public void Error(Exception e)
        {
            Error(_tag, e);
        }

        public void Error(string tag, Exception e)
        {
            Log.Error(tag ?? _tag, $"error={e.Message};stack={e.StackTrace}");
        }

        public void Error(string msg)
        {
            Error(_tag, msg);
        }

        public void Error(string tag, string msg)
        {
            InternalLog(tag, msg, (t, m) => Log.Error(t ?? _tag, m));
        }

        public void Info(string msg)
        {
            Info(_tag, msg);
        }

        public void Info(string tag, string msg)
        {
            InternalLog(tag, msg, (t, m) => Log.Info(t ?? _tag, m));
        }

        private void InternalLog(string tag, string msg, Action<string, string> log)
        {
            if (string.IsNullOrEmpty(msg)) return;

            var lines = msg.Split('\n');
            foreach (var line in lines) log(tag ?? _tag, line);
        }

        public void Warning(Exception e)
        {
            Warning(_tag, e);
        }

        public void Warning(string tag, Exception e)
        {
            Log.Warn(tag ?? _tag, $"error={e.Message};stack={e.StackTrace}");
        }

        public void Warning(string msg)
        {
            Warning(_tag, msg);
        }

        public void Warning(string tag, string msg)
        {
            InternalLog(tag, msg, (t, m) => Log.Warn(t ?? _tag, m));
        }
    }
}