using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfManager
    {
        Task ProcessDelivery(string deliveryNumber, string driverName);
    }
}