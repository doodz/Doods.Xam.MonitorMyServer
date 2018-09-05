using NLog;
using System;
using ILogger = Doods.Framework.Std.ILogger;

namespace Doods.Xam.MonitorMyServer.iOS.Services
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _log;

        public Logger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Error(string msg)
        {
            _log.Error(msg);
        }

        public void Info(string msg)
        {
            _log.Info(msg);
        }

        public void Warning(string msg)
        {
            _log.Warn(msg);
        }

        public void Warning(Exception e)
        {
            _log.Warn(e);
        }

       
        public void Error(Exception e)
        {
            _log.Error(e);
        }
    }
}