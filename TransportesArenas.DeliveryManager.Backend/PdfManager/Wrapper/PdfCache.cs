using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfCache: IPdfCache
    {
        public PdfCache()
        {
            this.WasProcessed = false;
        }

        public int Page { get; set; }
        public string Content { get; set; }
        public bool WasProcessed { get; set; }
    }
}