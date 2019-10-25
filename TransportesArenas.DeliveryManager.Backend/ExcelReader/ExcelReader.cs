using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class ExcelReader : IExcelReader
    {
        public async Task<List<IDelivery>> GetDeliveriesAsync(string sourceFileName)
        {
            var deliveries = new List<IDelivery>();
            using (var excelPackage = new ExcelPackage(new FileInfo(sourceFileName)))
            {
                var worksheet = excelPackage.Workbook.Worksheets[1];

                var rows = worksheet.Dimension.Rows;

                for (var row = 2; row <= rows; row++)
                {
                    var delivery = new Delivery();

                    delivery.PickUpDate = Convert.ToDateTime(worksheet.Cells[row, 1].Value.ToString());
                    delivery.DriverName = worksheet.Cells[row, 2].Value.ToString();
                    delivery.DeliveryReference = worksheet.Cells[row, 3].Value.ToString();
                    delivery.PickUpCompany = worksheet.Cells[row, 4].Value.ToString();
                    delivery.PickUpCity = worksheet.Cells[row, 5].Value.ToString();
                    delivery.DropOffCompany = worksheet.Cells[row, 6].Value.ToString();
                    delivery.DropOffCity = worksheet.Cells[row, 7].Value.ToString();
                    delivery.Weight = Convert.ToInt32(worksheet.Cells[row, 8].Value.ToString());

                    deliveries.Add(delivery);
                }
            }

            return await Task.FromResult(deliveries);
        }
    }
}