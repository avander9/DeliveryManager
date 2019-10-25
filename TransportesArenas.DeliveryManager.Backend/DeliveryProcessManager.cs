using System;
using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryProcessManager : IDeliveryProcessManager
    {
        public event TotalDeliveriesEvent TotalDeliveriesEvent;
        public event StepEvent StepEvent;

        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            var deliveries = await new ExcelReader().GetDeliveriesAsync(request.ExcelFile).ConfigureAwait(true);
            this.OnTotalDeliveriesEvent(deliveries.Count);

            var pdfManager = new PdfManager(request.PdfFile, request.OutputFolder);

            foreach (var delivery in deliveries)
            {
                this.OnStepEvent();
                await pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName).ConfigureAwait(false);
            }
        }


        protected virtual void OnTotalDeliveriesEvent(int deliveries)
        {
            TotalDeliveriesEvent?.Invoke(deliveries);
        }

        protected virtual void OnStepEvent()
        {
            StepEvent?.Invoke();
        }
    }
}