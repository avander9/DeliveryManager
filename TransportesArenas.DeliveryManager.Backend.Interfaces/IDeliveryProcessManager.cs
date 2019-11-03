using System;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public delegate void TotalDeliveriesEvent(int deliveries);
    public delegate void StepEvent();
    public delegate void ProcessEndedEvent();

    public interface IDeliveryProcessManager: IDisposable
    {
        event TotalDeliveriesEvent TotalDeliveriesEvent;
        event StepEvent StepEvent;
        event ProcessEndedEvent ProcessEndedEvent;
        Task RunAsync(IDelivaryManagerProcessRequest request);
    }
}