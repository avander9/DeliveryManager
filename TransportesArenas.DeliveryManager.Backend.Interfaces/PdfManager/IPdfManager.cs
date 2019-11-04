using System;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfManager: IDisposable
    {
        bool ProcessDelivery(string deliveryNumber, string driverName);
        void SetParameters(string requestPdfFile, string requestOutputFolder);
    }
}