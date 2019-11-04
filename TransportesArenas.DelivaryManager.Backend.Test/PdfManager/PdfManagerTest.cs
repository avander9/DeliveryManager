using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DelivaryManager.Backend.Test
{
    [TestClass]
    public class PdfManagerTest
    {
        private readonly IPdfManager manager;
        private readonly IPdfWrapper wrapper;

        public PdfManagerTest()
        {
            var destinationFolder = @"c:\test\";
            this.wrapper = Substitute.For<IPdfWrapper>();
            this.manager = new PdfManager(this.wrapper);
        }

        [TestMethod]
        public void ProcessDelivery_DeliveryNotFound()
        {
            this.wrapper.FindValueAsync(Arg.Any<string>()).Returns(x => new PdfResult());

            this.manager.ProcessDelivery("TestDelivery", "TestDriver");

            this.wrapper.Received().FindValueAsync("TestDelivery");
        }

        [TestMethod]
        public void ProcessDelivery_DeliveryFound()
        {
            var fileName = @"c:\test\TestDriver\TestDeliveryNumber.pdf";
            this.wrapper.FindValueAsync(Arg.Any<string>()).Returns(x => new PdfResult
            {
                Found = true,
                Page = 1
            });

            this.manager.ProcessDelivery("TestDeliveryNumber", "TestDriver");

            this.wrapper.Received().FindValueAsync("TestDeliveryNumber");
            this.wrapper.Received().SplitAndSave(fileName, 1);
        }
    }
}