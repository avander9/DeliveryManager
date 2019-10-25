namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfResult
    {
        bool Found { get; set; }
        int Page { get; set; }
    }
}