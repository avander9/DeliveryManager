using System;
using log4net;
using log4net.Config;
using log4net.Util;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryManagerLogger : IDeliveryManagerLogger
    {
        private readonly ILog logger;

        public DeliveryManagerLogger(ILog logger)
        {
            this.logger = logger;
        }

        public DeliveryManagerLogger()
        {
            this.logger = LogManager.GetLogger(typeof(DeliveryProcessManager));
            //GlobalContext.Properties["LogFileName"] = $"LogMessage-{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}";
            XmlConfigurator.Configure();
        }

        public void LogMessage(string message, LogType type = LogType.Debug)
        {
            switch (type)
            {
                case LogType.Debug:
                    this.LogDebugMessage(message);
                    break;
                case LogType.Info:
                    this.logger.Info(message);
                    break;
                case LogType.Warning:
                    this.logger.Warn(message);
                    break;
                case LogType.Error:
                    this.logger.Error(message);
                    break;
                case LogType.Fatal:
                    this.logger.Fatal(message);
                    break;
            }
        }
        public void LogException(string message, Exception exception)
        {
            this.logger.FatalFormat(message, exception);
        }

        private void LogDebugMessage(string message)
        {
            if(this.logger.IsDebugEnabled)
                this.logger.Debug(message);
        }
    }
}