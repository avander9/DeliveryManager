namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfManagerMapper
    {
        IPdfResult MapFrom(IPdfCache cacheItem);
    }
}