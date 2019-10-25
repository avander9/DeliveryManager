using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryProcessManager : IDeliveryProcessManager
    {
        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            var deliveries = await new ExcelReader().GetDeliveriesAsync(request.ExcelFile).ConfigureAwait(true);

            var pdfManager = new PdfManager(request.PdfFile, request.OutputFolder);

            foreach (var delivery in deliveries)
            {
                await pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName).ConfigureAwait(true);
            }
        }
    }
}