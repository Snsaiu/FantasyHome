using System;
using System.IO;
using log4net;
using log4net.Config;
using LogManager = log4net.LogManager;

namespace FantasyRoomDisplayDevice.Services
{
    public class Logger : ILogger
    {
        private ILog _debugLogger;
        private ILog _errorLogger;
        private ILog _infoLogger;
        private ILog _warnLogger;

       


        public Logger()
        {
            var logCfg = new FileInfo(Path.Combine( AppDomain.CurrentDomain.BaseDirectory , "logging.config"));
            XmlConfigurator.ConfigureAndWatch(logCfg);

            _debugLogger = LogManager.GetLogger("logdebug");
            _errorLogger = LogManager.GetLogger("logerror");
            _infoLogger = LogManager.GetLogger("loginfo");
            _warnLogger = LogManager.GetLogger("logwarn");
        }

        public void Debug(string message)
        {
            this._debugLogger.Debug(message);
        }

        public void Error(string message)
        {
            this._errorLogger.Error(message);
        }

        public void Info(string message)
        {
            this._infoLogger.Info(message);
        }


        public void Warning(string message)
        {
            this._warnLogger.Warn(message);
        }
    }
}