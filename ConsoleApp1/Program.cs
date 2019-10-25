using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var pdfFilePath = @"C:\Users\alexa\Downloads\MAYO 2019 GRABUS.pdf";
            var outputPath = @"C:\Users\alexa\Downloads\SplitedPdfFiles";

            using (var reader = new PdfReader(pdfFilePath))
            {
                ITextExtractionStrategy extractionStrategy = new SimpleTextExtractionStrategy();

                var file = new FileInfo(pdfFilePath);
                var pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".", StringComparison.Ordinal)) + "-";
                
                for (var pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
                {
                    var textFromPage = PdfTextExtractor.GetTextFromPage(reader, pageNumber);

                    var x = extractionStrategy.GetResultantText();

                    textFromPage = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8,
                        Encoding.Default.GetBytes(textFromPage)));

                    if (textFromPage.Contains("gava"))
                    {
                        var newPdfFileName = string.Format($"{pdfFileName} + {pageNumber}");
                        SplitAndSaveInterval(pdfFilePath, outputPath, pageNumber, newPdfFileName);
                    }
                }
            }
        }

        private static void SplitAndSaveInterval(string pdfFilePath, string outputPath, int pageNumber, string pdfFileName)
        {
            using (var reader = new PdfReader(pdfFilePath))
            {
                var document = new Document();
                var copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();

                copy.AddPage(copy.GetImportedPage(reader, pageNumber));

                document.Close();
            }
        }
    }
}
