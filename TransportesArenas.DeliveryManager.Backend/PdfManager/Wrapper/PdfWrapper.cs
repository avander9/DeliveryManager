using System;
using System.Collections.Generic;
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
        private List<IPdfCache> cache;

        public IPdfWrapper Build(string filePath)
        {
            this.reader = new PdfReader(filePath);
            this.cache = new List<IPdfCache>();
            this.LoadCache();
            return this;
        }

        /// <inheritdoc />
        public string ReadPage(int pageNumber)
        {
            var textFromPage = PdfTextExtractor.GetTextFromPage(this.reader, pageNumber);

            textFromPage = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default,
                Encoding.UTF8,
                Encoding.Default.GetBytes(textFromPage)));

            return textFromPage;
        }

        /// <inheritdoc />
        public IPdfResult FindValueAsync(string value)
        {
            var pdfResult = new PdfResult();

            var pdfCache = this.cache.Find(x => x.Content.Contains(value));

            if (pdfCache != null)
            {
                pdfResult.Found = true;
                pdfResult.Page = pdfCache.Page;
            }

            return pdfResult;
        }

        /// <inheritdoc />
        public void SplitAndSave(string outputFile, int pageNumber)
        {
            using (var document = new Document())
            {
                var copy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));
            }
        }

        private void LoadCache()
        {
            for (int page = 1; page < reader.NumberOfPages; page++)
            {
                var content = this.ReadPage(page);

                this.cache.Add(new PdfCache
                {
                    Content = content,
                    Page = page
                });
            }
        }

        public void Dispose()
        {
            this.reader?.Dispose();
            this.cache?.Clear();
        }
    }
}