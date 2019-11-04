using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DelivaryManager.Backend.Test
{
    [TestClass]
    public class ExcelReaderTest
    {
        private IExcelReader excelReader;

        public ExcelReaderTest()
        {
            this.excelReader = new ExcelReader();
        }

        [TestMethod]
        public void GetDeliveriesAsync()
        {

        }
    }
}