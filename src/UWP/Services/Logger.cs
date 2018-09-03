using NLog;
using System;
using System.Collections.Generic;
using ILogger = Doods.Framework.Std.ILogger;

namespace Doods.Xam.MonitorMyServer.UWP.Services
{
    internal class Logger : ILogger
    {
        private const string ProjectName = "MonitorMyServer";

        private readonly NLog.Logger _log;

        public Logger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            _log?.Debug(msg);
            System.Diagnostics.Trace.TraceInformation($"{ProjectName} : {msg}");
        }

        public void Event(string type, Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception e)
        {
            _log?.Error(e);
            System.Diagnostics.Trace.TraceError($"{ProjectName} : error={e.Message};stack={e.StackTrace}");
        }

        public void Error(string msg)
        {
            _log?.Error(msg);
            System.Diagnostics.Trace.TraceError($"{ProjectName} : {msg}");
        }

        public void Error(Exception e, string type = null)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            _log?.Info(msg);
            System.Diagnostics.Trace.TraceInformation($"{ProjectName} : {msg}");
        }

        public void Warning(Exception e)
        {
            _log?.Warn(e);
            System.Diagnostics.Trace.TraceWarning($"{ProjectName} : error={e.Message};stack={e.StackTrace}");
        }

        public void Warning(string msg)
        {
            _log?.Warn(msg);
            System.Diagnostics.Trace.TraceWarning($"{ProjectName} : {msg}");
        }
    }
}