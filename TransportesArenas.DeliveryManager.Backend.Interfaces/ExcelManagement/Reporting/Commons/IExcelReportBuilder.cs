using System.Collections.Generic;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IExcelReportBuilder
    {
        void Build(string outputFile, List<IDelivery> deliveriesToPrint);
    }
}