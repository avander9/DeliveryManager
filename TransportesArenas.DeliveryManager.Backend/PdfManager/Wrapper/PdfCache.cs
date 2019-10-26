using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfCache: IPdfCache
    {
        public int Page { get; set; }
        public string Content { get; set; }
    }
}