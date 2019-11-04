using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DelivaryManager.Backend.Test.ProcessManager
{
    [TestClass]
    public class DelivaryManagerProcessTest
    {
        private IPdfManager pdfManager;
        private IDeliveryManagerLogger logger;
        private IExcelReportBuilder reportBuilder;
        private IExcelReader excelReader;
        private IDeliveryProcessManager processManager;

        public DelivaryManagerProcessTest()
        {
            this.pdfManager = Substitute.For<IPdfManager>();
            this.logger = Substitute.For<IDeliveryManagerLogger>();
            this.reportBuilder = Substitute.For<IExcelReportBuilder>();
            this.excelReader = Substitute.For<IExcelReader>();

            this.processManager = new DeliveryProcessManager(this.pdfManager, 
                this.logger, 
                this.reportBuilder, 
                this.excelReader);
        }

        ~DelivaryManagerProcessTest()
        {
            this.pdfManager = null;
            this.logger = null;
            this.reportBuilder = null;
            this.excelReader = null;
            this.processManager = null;
        }

        [TestMethod]
        public async Task RunAsync_OK()
        {
            var deliveriesList = new List<IDelivery>
            {
                new Delivery
                {
                    DeliveryReference = "TestDelivery",
                    DriverName = "TestDriver"
                }
            };

            this.excelReader.GetDeliveriesAsync(Arg.Any<string>()).Returns(x=> deliveriesList);
            this.pdfManager.ProcessDelivery("TestDelivery", "TestDriver").Returns(true);

            var resquest = new DelivaryManagerProcessRequest
            {
                ExcelFile = "TestExcelFile.xlsx",
                PdfFile = "TestPdfFile.pdf",
                OutputFolder = @"c:\TestOutputFile"
            };

            var totalDeliveriesEventWasRaised = false;
            this.processManager.TotalDeliveriesEvent += (o) => totalDeliveriesEventWasRaised = true;

            var stepEventWasRaised = false;
            this.processManager.StepEvent += () => stepEventWasRaised = true;

            var processEndedEventWasRaised = false;
            this.processManager.ProcessEndedEvent += () => processEndedEventWasRaised = true;

            await this.processManager.RunAsync(resquest);

            Assert.IsTrue(totalDeliveriesEventWasRaised);
            Assert.IsTrue(stepEventWasRaised);
            Assert.IsTrue(processEndedEventWasRaised);

            await this.excelReader.Received(1).GetDeliveriesAsync(resquest.ExcelFile);
            this.pdfManager.Received(1).SetParameters(resquest.PdfFile, resquest.OutputFolder);
            this.pdfManager.Received(1).ProcessDelivery("TestDelivery", "TestDriver");
            this.reportBuilder.Received(0).Build(Arg.Any<string>(), Arg.Any<List<IDelivery>>());
        }

        [TestMethod]
        public async Task RunAsync_DeliveryNotFound()
        {
            var deliveriesList = new List<IDelivery>
            {
                new Delivery
                {
                    DeliveryReference = "TestDelivery",
                    DriverName = "TestDriver"
                }
            };

            this.excelReader.GetDeliveriesAsync(Arg.Any<string>()).Returns(x=> deliveriesList);
            this.pdfManager.ProcessDelivery("TestDelivery", "TestDriver").Returns(false);

            var resquest = new DelivaryManagerProcessRequest
            {
                ExcelFile = "TestExcelFile.xlsx",
                PdfFile = "TestPdfFile.pdf",
                OutputFolder = @"c:\TestOutputFile"
            };

            var totalDeliveriesEventWasRaised = false;
            this.processManager.TotalDeliveriesEvent += (o) => totalDeliveriesEventWasRaised = true;

            var stepEventWasRaised = false;
            this.processManager.StepEvent += () => stepEventWasRaised = true;

            var processEndedEventWasRaised = false;
            this.processManager.ProcessEndedEvent += () => processEndedEventWasRaised = true;

            await this.processManager.RunAsync(resquest);

            Assert.IsTrue(totalDeliveriesEventWasRaised);
            Assert.IsTrue(stepEventWasRaised);
            Assert.IsTrue(processEndedEventWasRaised);

            await this.excelReader.Received(1).GetDeliveriesAsync(resquest.ExcelFile);
            this.pdfManager.Received(1).SetParameters(resquest.PdfFile, resquest.OutputFolder);
            this.pdfManager.Received(1).ProcessDelivery("TestDelivery", "TestDriver");
            this.reportBuilder.Received(1).Build(Arg.Any<string>(), Arg.Any<List<IDelivery>>());
            this.logger.Received(1).LogMessage(Arg.Any<string>(), LogType.Warning);
        }
    }
}