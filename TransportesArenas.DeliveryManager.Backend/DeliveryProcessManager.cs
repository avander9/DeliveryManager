using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryProcessManager : IDeliveryProcessManager
    {
        private IPdfManager pdfManager;
        private DelireviesMissingReportExcelGenerator reportExcelGenerator;
        public event TotalDeliveriesEvent TotalDeliveriesEvent;
        public event StepEvent StepEvent;

        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            try
            {
                var deliveries = await new ExcelReader()
                    .GetDeliveriesAsync(request.ExcelFile)
                    .ConfigureAwait(true);

                var deliveriesNotProcessed = new List<IDelivery>();
                this.OnTotalDeliveriesEvent(deliveries.Count);

                this.pdfManager = new PdfManager(request.PdfFile, request.OutputFolder);

                foreach (var delivery in deliveries)
                {
                    this.OnStepEvent();
                    var processed = this.pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName);

                    if (!processed)
                        deliveriesNotProcessed.Add(delivery);
                }
                deliveries.Clear();

                if (deliveriesNotProcessed.Any())
                    this.GenerateReport(request.OutputFolder, deliveriesNotProcessed);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void GenerateReport(string requestOutputFolder, List<IDelivery> deliveriesNotProcessed)
        {
            var outputFile = Path.Combine(requestOutputFolder, GetNotProcessedExcelFileName());
            this.reportExcelGenerator = new DelireviesMissingReportExcelGenerator();
            this.reportExcelGenerator.GenerateReport(outputFile, deliveriesNotProcessed);

        }

        private string GetNotProcessedExcelFileName()
        {
            return
                $"No Procesados {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-{DateTime.Now.Hour}{DateTime.Now.Minute}.xlsx";
        }


        protected virtual void OnTotalDeliveriesEvent(int deliveries)
        {
            TotalDeliveriesEvent?.Invoke(deliveries);
        }

        protected virtual void OnStepEvent()
        {
            StepEvent?.Invoke();
        }

        public void Dispose()
        {
            this.pdfManager?.Dispose();
            this.reportExcelGenerator?.Dispose();
        }
    }
}