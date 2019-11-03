using System;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IDeliveryManagerLogger
    {
        IDeliveryManagerLogger Build();
        void LogMessage(string message, LogType type = LogType.Debug);
        void LogException(string message, Exception exception);
    }
}