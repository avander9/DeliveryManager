using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfManagerMapper: IPdfManagerMapper
    {
        public IPdfResult MapFrom(IPdfCache cacheItem)
        {
            var pdfResult = new PdfResult
            {
                Page = cacheItem.Page
            };

            return pdfResult;
        }
    }
}