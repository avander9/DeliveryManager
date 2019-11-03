using System;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public delegate void TotalDeliveriesEvent(int deliveries);
    public delegate void StepEvent();
    public interface IDeliveryProcessManager: IDisposable
    {
        event TotalDeliveriesEvent TotalDeliveriesEvent;
        event StepEvent StepEvent;
        Task RunAsync(IDelivaryManagerProcessRequest request);
    }
}