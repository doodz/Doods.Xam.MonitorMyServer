using NLog;
using System;
using System.Collections.Generic;
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

        public void Event(string type, Dictionary<string, string> properties = null, Dictionary<string, double> measures = null)
        {
            throw new NotImplementedException();
            ////DOTO: Inclure des mesures /propriétés par défaut ici 
            //if (properties == null)
            //{
            //    properties = new Dictionary<string, string>();
            //}

            //properties.Add("location", App.LocationChangedEvent?.Location?.ToString());
            ////HockeyApp.MetricsManager.TrackEvent(type, properties, measures);
        }

        public void Error(Exception e, string type = null)
        {
            _log.Error(e);
        }
    }
}