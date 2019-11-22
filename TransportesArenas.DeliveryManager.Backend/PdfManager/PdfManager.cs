using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfManager: IPdfManager
    {
        private string destinationFolder;
        private readonly IPdfWrapper pdfWrapper;

        public PdfManager(IPdfWrapper pdfWrapper)
        {
            this.pdfWrapper = pdfWrapper ?? throw new ArgumentException(nameof(pdfWrapper));
        }

        public bool ProcessDelivery(string deliveryNumber, string driverName)
        {
            var result = this.pdfWrapper.FindValueAsync(deliveryNumber);

            if (result.Found)
            {
                var fileName = Path.Combine(this.GetDriverFolder(driverName), deliveryNumber + ".pdf");
                this.pdfWrapper.SplitAndSave(fileName, result.Page);
            }

            return result.Found;
        }

        public void SetParameters(string pdfSourceFile, string requestOutputFolder)
        {
            this.destinationFolder = requestOutputFolder;
            this.pdfWrapper.Build(pdfSourceFile);
        }

        public void PrintNotProcessedPages()
        {
            var outputFile = Path.Combine(this.destinationFolder, this.GetNotProcessedFileName());
            this.pdfWrapper.ExtractNotProcessedPages(outputFile);
        }

        private string GetNotProcessedFileName()
        {
            return
                $"No Procesados {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-{DateTime.Now.Hour}{DateTime.Now.Minute}.pdf";
        }

        private string GetDriverFolder(string driverName)
        {
            var driverFolder = this.BuildDriverFolder(driverName);

            if (Directory.Exists(driverFolder) == false)
                Directory.CreateDirectory(driverFolder);

            return driverFolder;
        }

        private string BuildDriverFolder(string driverName)
        {
            return Path.Combine(this.destinationFolder, driverName.ToUpper());
        }

        public void Dispose()
        {
            this.pdfWrapper?.Dispose();
        }
    }
}