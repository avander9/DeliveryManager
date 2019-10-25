namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IDelivaryManagerProcessRequest
    {
        string ExcelFile { get; set; }
        string PdfFile { get; set; }
        string OutputFolder { get; set; }
    }
}