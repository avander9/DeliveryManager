using System.Threading.Tasks;
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
            this.manager = new PdfManager(this.wrapper, destinationFolder);
        }

        [TestMethod]
        public async Task ProcessDelivery_DeliveryNotFound()
        {
            this.wrapper.FindValueAsync(Arg.Any<string>()).Returns(x => new PdfResult());

            await this.manager.ProcessDelivery("TestDelivery", "TestDriver").ConfigureAwait(false);

            await this.wrapper.Received().FindValueAsync("TestDelivery").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ProcessDelivery_DeliveryFound()
        {
            var fileName = @"c:\test\TestDriver\TestDeliveryNumber.pdf";
            this.wrapper.FindValueAsync(Arg.Any<string>()).Returns(x => new PdfResult
            {
                Found = true,
                Page = 1
            });

            await this.manager.ProcessDelivery("TestDeliveryNumber", "TestDriver").ConfigureAwait(false);

            await this.wrapper.Received().FindValueAsync("TestDeliveryNumber").ConfigureAwait(false);
            this.wrapper.Received().SplitAndSave(fileName, 1);
        }
    }
}