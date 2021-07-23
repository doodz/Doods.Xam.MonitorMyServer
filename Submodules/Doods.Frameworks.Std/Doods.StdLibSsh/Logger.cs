using System;

namespace Doods.StdLibSsh
{
    internal class Logger
    {
        private static Logger _instance = null;
        private static readonly object Padlock = new object();
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Padlock)
                    {
                        if (_instance == null)
                        {
                            //NLog.LogManager.Configuration =
                            //    new NLog.Config.XmlLoggingConfiguration("NLog.config", true);
                             _instance = new Doods.StdLibSsh.Logger();
                        }
                        return _instance;
                    }
                }
                return _instance;
            }
        }

        //private readonly NLog.Logger _log;

        private Logger()
        {
           // _log = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            //_log.Debug(msg);
        }

        public void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            //_log.Error(e);
        }

        public void Error(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            //_log.Error(msg);
        }

        public void Info(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            //_log.Info(msg);
        }

        public void Warning(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            //_log.Warn(e);
        }

        public void Warning(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            //_log.Warn(msg);
        }
    }
}
