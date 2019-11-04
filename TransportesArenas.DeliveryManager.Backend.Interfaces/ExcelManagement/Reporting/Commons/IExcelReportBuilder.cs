using System;
using System.Collections.Generic;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IExcelReportBuilder: IDisposable
    {
        void Build(string outputFile, List<IDelivery> deliveriesToPrint);
    }
}