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
        private IPdfManager pdfManager;
        private DelireviesMissingReportExcelGenerator reportExcelGenerator;
        public event TotalDeliveriesEvent TotalDeliveriesEvent;
        public event StepEvent StepEvent;
        public event ProcessEndedEvent ProcessEndedEvent;
        private ILog logger;

        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            try
            {
                this.logger = LogManager.GetLogger(typeof(DeliveryProcessManager));
                //GlobalContext.Properties["LogFileName"] = $"Log-{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}";
                XmlConfigurator.Configure();
                this.logger.Info("Iniciando Proceso");

                var deliveries = await new ExcelReader()
                    .GetDeliveriesAsync(request.ExcelFile)
                    .ConfigureAwait(true);
                this.logger.Info($"{deliveries.Count} albaranes leídos desde excel");

                var deliveriesNotProcessed = new List<IDelivery>();
                this.OnTotalDeliveriesEvent(deliveries.Count);

                this.pdfManager = new PdfManager(request.PdfFile, request.OutputFolder);

                this.logger.Info("Empezamos a procesar albaranes");
                foreach (var delivery in deliveries)
                {
                    this.OnStepEvent();
                    var processed = this.pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName);

                    if (!processed)
                    {
                        this.logger.Info($"Albarán {delivery.DeliveryReference} No encontrado");
                        deliveriesNotProcessed.Add(delivery);
                    }
                }
                deliveries.Clear();

                if (deliveriesNotProcessed.Any())
                    this.GenerateReport(request.OutputFolder, deliveriesNotProcessed);

                this.OnProcessEndedEvent();
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message, exception);
            }
        }

        private void GenerateReport(string requestOutputFolder, List<IDelivery> deliveriesNotProcessed)
        {
            this.logger.Info("Generando Reporte");
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