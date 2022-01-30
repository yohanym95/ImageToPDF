namespace ImageToPdf.Services;

public interface IPDFService
{
    public Stream ImageToPdf(string filename, string pdfname, Stream file);
    public void SplitPDF(string filename, string pdfname, Stream file);
}