using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private PdfReader pdfReader;
        private List<IPdfCache> cache;
        private readonly IPdfManagerMapper mapper;

        public PdfWrapper(IPdfManagerMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IPdfWrapper Build(string filePath)
        {
            this.pdfReader = new PdfReader(filePath);
            this.cache = new List<IPdfCache>();
            this.LoadCache();
            return this;
        }

        /// <inheritdoc />
        public string ReadPage(int pageNumber)
        {
            var textFromPage = PdfTextExtractor.GetTextFromPage(this.pdfReader, pageNumber);

            textFromPage = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default,
                Encoding.UTF8,
                Encoding.Default.GetBytes(textFromPage)));

            return textFromPage;
        }

        /// <inheritdoc />
        public IPdfResult FindValueAsync(string value)
        {
            var pdfResult = new PdfResult();

            var pdfCache = this.GetFromCache(value);

            if (pdfCache != null)
            {
                pdfResult.Found = true;
                pdfResult.Page = pdfCache.Page;
                this.SetPageAsProcessed(pdfCache.Page);
            }

            return pdfResult;
        }

        /// <inheritdoc />
        public void SplitAndSave(string outputFile, int pageNumber)
        {
            using (var document = new Document())
            {
                this.CopyPage(outputFile, pageNumber, document);
            }
        }

        public void ExtractNotProcessedPages(string outputFile)
        {
            var pdfCachesNotProcessed = this.cache.FindAll(x => !x.WasProcessed);
            
            using (var document = new Document())
            {
                var copy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));
                document.Open();
                
                foreach (var cacheItem in pdfCachesNotProcessed)
                {
                    copy.AddPage(copy.GetImportedPage(this.pdfReader, cacheItem.Page));
                }
            }
        }

        private void CopyPage(string outputFile, int pageNumber, Document document)
        {
            var copy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));
            document.Open();
            copy.AddPage(copy.GetImportedPage(this.pdfReader, pageNumber));
        }

        private void LoadCache()
        {
            for (int page = 1; page < this.pdfReader.NumberOfPages; page++)
            {
                var content = this.ReadPage(page);

                this.cache.Add(new PdfCache
                {
                    Content = content,
                    Page = page
                });
            }
        }

        private IPdfCache GetFromCache(string value)
        {
            var result = this.cache.Find(x => x.Content.Contains(value));

            return result;
        }

        private void SetPageAsProcessed(int resultPage)
        {
            this.cache.Find(x => x.Page == resultPage).WasProcessed = true;
        }

        public void Dispose()
        {
            this.pdfReader?.Dispose();
            this.cache?.Clear();
        }
    }
}