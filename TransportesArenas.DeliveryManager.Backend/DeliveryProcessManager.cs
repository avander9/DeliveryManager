using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryProcessManager : IDeliveryProcessManager
    {
        public event TotalDeliveriesEvent TotalDeliveriesEvent;
        public event StepEvent StepEvent;
        public event ProcessEndedEvent ProcessEndedEvent;

        private readonly IPdfManager pdfManager;
        private readonly DelireviesMissingReportExcelGenerator reportExcelGenerator;
        private readonly IExcelReader excelReader;
        private readonly IDeliveryManagerLogger logger;
        
        public DeliveryProcessManager(IPdfManager pdfManager, 
            IDeliveryManagerLogger logger, 
            DelireviesMissingReportExcelGenerator reportExcelGenerator,
            IExcelReader excelReader)
        {
            this.pdfManager = pdfManager;
            this.logger = logger;
            this.reportExcelGenerator = reportExcelGenerator;
            this.excelReader = excelReader;
        }

        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            try
            {
                this.logger.LogMessage("Iniciando Proceso");

                var deliveries = await this.excelReader
                    .GetDeliveriesAsync(request.ExcelFile)
                    .ConfigureAwait(true);
                this.logger.LogMessage($"{deliveries.Count} albaranes leídos desde excel");

                var deliveriesNotProcessed = new List<IDelivery>();
                this.OnTotalDeliveriesEvent(deliveries.Count);

                this.pdfManager.SetParameters(request.PdfFile, request.OutputFolder);

                this.logger.LogMessage("Empezamos a procesar albaranes");
                foreach (var delivery in deliveries)
                {
                    this.OnStepEvent();
                    var processed = this.pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName);

                    if (!processed)
                    {
                        this.logger.LogMessage($"Albarán {delivery.DeliveryReference} No encontrado");
                        deliveriesNotProcessed.Add(delivery);
                    }
                }
                deliveries.Clear();

                if (deliveriesNotProcessed.Any())
                    this.GenerateReport(request.OutputFolder, deliveriesNotProcessed);

                this.OnProcessEndedEvent();
                this.logger.LogMessage("Proceso Finalizado");
            }
            catch (Exception exception)
            {
                this.logger.LogException(exception.Message, exception);
            }
        }

        private void GenerateReport(string requestOutputFolder, List<IDelivery> deliveriesNotProcessed)
        {
            this.logger.LogMessage("Generando Reporte");
            var outputFile = Path.Combine(requestOutputFolder, GetNotProcessedExcelFileName());
            //this.reportExcelGenerator = new DelireviesMissingReportExcelGenerator();
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

        protected virtual void OnProcessEndedEvent()
        {
            ProcessEndedEvent?.Invoke();
        }

        public void Dispose()
        {
            this.pdfManager?.Dispose();
            this.reportExcelGenerator?.Dispose();
        }
    }
}