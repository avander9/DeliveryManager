﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DeliveryProcessManager : IDeliveryProcessManager
    {
        public event TotalDeliveriesEvent TotalDeliveriesEvent;
        public event StepEvent StepEvent;
        public event ProcessEndedEvent ProcessEndedEvent;

        private readonly IPdfManager pdfManager;
        private readonly IExcelReportBuilder excelReportBuilder;
        private readonly IExcelReader excelReader;
        private readonly IDeliveryManagerLogger logger;
        
        public DeliveryProcessManager(IPdfManager pdfManager, 
            IDeliveryManagerLogger logger, 
            IExcelReportBuilder excelReportBuilder,
            IExcelReader excelReader)
        {
            this.pdfManager = pdfManager;
            this.logger = logger;
            this.excelReportBuilder = excelReportBuilder;
            this.excelReader = excelReader;
        }

        public async Task RunAsync(IDelivaryManagerProcessRequest request)
        {
            try
            {
                var deliveries = await this.GetDeliveriesAsync(request);
                var deliveriesNotProcessed = new List<IDelivery>();
                
                this.OnTotalDeliveriesEvent(deliveries.Count);

                this.pdfManager.SetParameters(request.PdfFile, request.OutputFolder);

                foreach (var delivery in deliveries)
                {
                    this.OnStepEvent();
                    var processed = this.pdfManager.ProcessDelivery(delivery.DeliveryReference, delivery.DriverName);

                    if (!processed)
                    {
                        this.logger.LogMessage($"Albarán {delivery.DeliveryReference} No encontrado", LogType.Warning);
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
                this.logger.LogException(exception.Message, exception);
            }
        }

        private async Task<List<IDelivery>> GetDeliveriesAsync(IDelivaryManagerProcessRequest request)
        {
            return await this.excelReader
                .GetDeliveriesAsync(request.ExcelFile)
                .ConfigureAwait(true);
        }

        private void GenerateReport(string requestOutputFolder, List<IDelivery> deliveriesNotProcessed)
        {
            this.logger.LogMessage("Generando Reporte");
            var outputFile = Path.Combine(requestOutputFolder, GetNotProcessedExcelFileName());
            this.excelReportBuilder.Build(outputFile, deliveriesNotProcessed);
        }

        private string GetNotProcessedExcelFileName()
        {
            return
                $"No Procesados {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-{DateTime.Now.Hour}{DateTime.Now.Minute}.xlsx";
        }

        protected virtual void OnTotalDeliveriesEvent(int deliveries)
        {
            this.TotalDeliveriesEvent?.Invoke(deliveries);
        }

        protected virtual void OnStepEvent()
        {
            this.StepEvent?.Invoke();
        }

        protected virtual void OnProcessEndedEvent()
        {
            this.ProcessEndedEvent?.Invoke();
        }

        public void Dispose()
        {
            this.pdfManager?.Dispose();
        }
    }
}