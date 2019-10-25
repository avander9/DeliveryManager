using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IDeliveryProcessManager
    {
        Task RunAsync(IDelivaryManagerProcessRequest request);
    }
}