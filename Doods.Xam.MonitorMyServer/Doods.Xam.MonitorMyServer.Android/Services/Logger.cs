using Android.Util;
using NLog;
using System;
using System.Collections.Generic;

namespace Doods.Xam.MonitorMyServer.Droid.Services
{
    public class Logger : Framework.Std.ILogger
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
            Log.Debug(ProjectName, msg);
        }

        public void Event(string type, Dictionary<string, string> properties = null, Dictionary<string, double> measures = null)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception e)
        {
            _log?.Error(e);
            Log.Error(ProjectName, $"error={e.Message};stack={e.StackTrace}");
        }

        public void Error(string msg)
        {
            _log?.Error(msg);
            Log.Error(ProjectName, msg);
        }

        public void Error(Exception e, string type = null)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            _log?.Info(msg);
            Log.Info(ProjectName, msg);
        }

        public void Warning(Exception e)
        {
            _log?.Warn(e);
            Log.Warn(ProjectName, $"error={e.Message};stack={e.StackTrace}");
        }

        public void Warning(string msg)
        {
            _log?.Warn(msg);
            Log.Warn(ProjectName, msg);
        }
    }
}