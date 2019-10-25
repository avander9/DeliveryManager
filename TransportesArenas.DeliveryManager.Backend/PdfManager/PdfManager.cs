using System.IO;
using System.Threading.Tasks;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations.PdfManager
{
    public class PdfManager: IPdfManager
    {
        private readonly string destinationFolder;
        private readonly IPdfWrapper pdfWrapper;

        public PdfManager(string pdfSourceFile, string destinationFolder)
        {
            this.destinationFolder = destinationFolder;
            this.pdfWrapper = new PdfWrapper()
                .SetFile(pdfSourceFile);
        }

        public PdfManager(IPdfWrapper wrapper, string destinationFolder)
        {
            this.destinationFolder = destinationFolder;
            this.pdfWrapper = wrapper;
        }

        public async Task ProcessDelivery(string deliveryNumber, string driverName)
        {
            var result = await this.pdfWrapper.FindValueAsync(deliveryNumber).ConfigureAwait(false);

            if (result.Found)
            {
                var fileName = Path.Combine(this.GetDriverFolder(driverName), deliveryNumber + ".pdf");
                await this.pdfWrapper.SplitAndSaveAsync(fileName, result.Page).ConfigureAwait(false);
            }
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
            return Path.Combine(this.destinationFolder, driverName);
        }
    }
}