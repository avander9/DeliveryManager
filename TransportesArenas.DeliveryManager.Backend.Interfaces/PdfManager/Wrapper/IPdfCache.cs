namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfCache
    {
        int Page { get; set; }
        string Content { get; set; }
    }
}