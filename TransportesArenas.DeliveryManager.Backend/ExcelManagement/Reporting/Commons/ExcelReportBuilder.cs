using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class ExcelReportBuilder: IExcelReportBuilder
    {
        private readonly DeliveriesMissingReportExcelGenerator excelGenerator;

        public ExcelReportBuilder(DeliveriesMissingReportExcelGenerator excelGenerator)
        {
            this.excelGenerator = excelGenerator;
        }

        public void Build(string outputFile, List<IDelivery> deliveriesToPrint)
        {
            var excelPackage = new ExcelPackage(new FileInfo(outputFile));
            var workbook = excelPackage.Workbook.Worksheets.Add("hoja 1");
            this.excelGenerator.GenerateReport(workbook, deliveriesToPrint);
            excelPackage.Save();
        }

    }
}