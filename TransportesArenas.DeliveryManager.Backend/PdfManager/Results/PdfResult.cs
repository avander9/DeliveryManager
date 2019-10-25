using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfResult: IPdfResult
    {
        public PdfResult()
        {
            this.Found = false;
            this.Page = 0;
        }

        public bool Found { get; set; }
        public int Page { get; set; }
    }
}