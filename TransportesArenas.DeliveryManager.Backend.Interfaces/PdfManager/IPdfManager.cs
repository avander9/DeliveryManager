using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfManager
    {
        void ProcessDelivery(string deliveryNumber, string driverName);
    }
}