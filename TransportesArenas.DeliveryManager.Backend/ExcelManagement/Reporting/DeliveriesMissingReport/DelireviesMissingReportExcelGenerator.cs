
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DelireviesMissingReportExcelGenerator: ExcelGeneratorBase
    {
        private List<IDelivery> deliveries;
        private ExcelWorksheet worksheet;

        public void GenerateReport(string outputFile, List<IDelivery> deliveriesToPrint)
        {
            this.deliveries = deliveriesToPrint;
            var reportFileInfo = new FileInfo(outputFile);
            this.ExcelPackage = new ExcelPackage(reportFileInfo);
            this.worksheet = this.ExcelPackage.Workbook.Worksheets.Add("hoja 1");
            this.Generate();
        }

        protected override void PrintColumnsHeader()
        {
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "pickup_date");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "driver_name");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "kn_reference");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "pickup_company");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "pickup_city");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "dropoff_company");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "dropoff_city");
            this.CurrentColumn++;
            this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, "weight");
        }

        protected override void PrintContent()
        {
            this.CurrentColumn = 1;
            this.CurrentRow = 1;
            foreach (var item in this.deliveries)
            {
                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.PickUpDate.ToShortDateString());
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.DriverName);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.DeliveryReference);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.PickUpCompany);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.PickUpCity);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.DropOffCompany);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.DropOffCity);
                this.CurrentColumn++;

                this.worksheet.SetValue(this.CurrentRow, this.CurrentColumn, item.Weight);

                this.CurrentRow++;
            }
        }

        protected override void SaveFile()
        {
            this.ExcelPackage.Save();
        }

        protected override void PrintHeader()
        {
            
        }
    }
}