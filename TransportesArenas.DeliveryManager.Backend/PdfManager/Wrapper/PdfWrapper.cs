using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class PdfWrapper : IPdfWrapper
    {
        private PdfReader reader;

        public IPdfWrapper SetFile(string filePath)
        {
            this.reader = new PdfReader(filePath);
            return this;
        }

        /// <inheritdoc />
        public async Task<string> ReadPage(int pageNumber)
        {
            var textFromPage = PdfTextExtractor.GetTextFromPage(reader, pageNumber);

            textFromPage = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default,
                Encoding.UTF8,
                Encoding.Default.GetBytes(textFromPage)));

            return await Task.FromResult(textFromPage);
        }

        /// <inheritdoc />
        public async Task<IPdfResult> FindValueAsync(string value)
        {
            var result = new PdfResult();
            for (var pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
            {
                var pageContent = await this.ReadPage(pageNumber).ConfigureAwait(true);

                if (!pageContent.Contains(value))
                    continue;

                result.Found = true;
                result.Page = pageNumber;
                return result;
            }

            return result;
        }

        /// <inheritdoc />
        public async Task SplitAndSaveAsync(string outputFile, int pageNumber)
        {
            Task.WaitAll();

            using (var document = new Document())
            {
                var copy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));
            }

        }

        public void Dispose()
        {
            reader?.Dispose();
        }
    }
}