using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace ImageToPdf.Services;

public class PDFService : IPDFService
{
    public Stream ImageToPdf(string filename, string pdfname, Stream file)
    {
        using (var document = new PdfDocument())
        {
            PdfPage page = document.AddPage();
            using (XImage img = XImage.FromStream(() => file))
            {
                var imageHeight = img.PixelHeight;
                var imageWidth = img.PixelWidth;
                int height;
                int width;
                int x;
                int y;

                width = 500;
                height = (int) Math.Ceiling((double) width * imageHeight / imageWidth);
                x = 50;
                y = (int) Math.Ceiling((800 - height) / 2.0);
        
                if(height > 700)
                {
                    height = 700;
                    width = (int) Math.Ceiling(imageWidth * (double) height / imageHeight);
                    y = 50;
                    x = (int) Math.Ceiling((600 - width) / 2.0);
                }
                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(img, x, y, width, height);
            }
            
            MemoryStream stream = new MemoryStream();
            file.CopyToAsync(stream);
            document.Save(stream);
            return stream;
        }
    }
    
    
    public void SplitPDF(string filename, string pdfname, Stream file)
    {
        PdfDocument pdfDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

        string name = filename;
        
        for (int idx = 0; idx < pdfDocument.PageCount; idx++)
        {
            // Create new document
            PdfDocument outputDocument = new PdfDocument();
            outputDocument.Version = pdfDocument.Version;
            outputDocument.Info.Title =
                String.Format("Page {0} of {1}", idx + 1, pdfDocument.Info.Title);
            outputDocument.Info.Creator = pdfDocument.Info.Creator;
 
            // Add the page and save it
            outputDocument.AddPage(pdfDocument.Pages[idx]);
            outputDocument.Save(String.Format("{0} - Page {1}.pdf", name, idx + 1));
        }
    }
}