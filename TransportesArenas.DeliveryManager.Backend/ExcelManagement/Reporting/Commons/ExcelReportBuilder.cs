using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class ExcelReportBuilder: IExcelReportBuilder
    {
        private readonly DeliveriesMissingReportExcelGenerator excelGenerator;
        private ExcelPackage excelPackage;

        public ExcelReportBuilder(DeliveriesMissingReportExcelGenerator excelGenerator)
        {
            this.excelGenerator = excelGenerator;
        }

        public void Build(string outputFile, List<IDelivery> deliveriesToPrint)
        {
            this.excelPackage = new ExcelPackage(new FileInfo(outputFile));
            var workbook = this.excelPackage.Workbook.Worksheets.Add("hoja 1");
            this.excelGenerator.GenerateReport(workbook, deliveriesToPrint);
            this.excelPackage.Save();
        }

        public void Dispose()
        {
            this.excelPackage?.Dispose();
        }
    }
}