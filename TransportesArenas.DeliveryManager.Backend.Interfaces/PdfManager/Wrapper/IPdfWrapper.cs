using System;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfWrapper: IDisposable
    {
        IPdfWrapper SetFile(string filePath);
        Task<string> ReadPage(int pageNumber);
        Task<IPdfResult> FindValueAsync(string value);
        Task SplitAndSaveAsync(string outputFile, int pageNumber);
    }
}