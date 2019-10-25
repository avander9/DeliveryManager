using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class DelivaryManagerProcessRequest: IDelivaryManagerProcessRequest
    {
        public string ExcelFile { get; set; }
        public string PdfFile { get; set; }
        public string OutputFolder { get; set; }
    }
}