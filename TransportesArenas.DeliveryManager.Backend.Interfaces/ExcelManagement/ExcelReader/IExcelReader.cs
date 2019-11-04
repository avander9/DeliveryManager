using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IExcelReader
    {
        Task<List<IDelivery>> GetDeliveriesAsync(string sourceFileName);
    }
}