using System.Diagnostics;
using ImageToPdf.Models;
using ImageToPdf.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageToPdf.Controllers;

[ApiController]
[Route("[controller]")]
public class PDFController : Controller
{
    private readonly IPDFService _pdfService;
    
    public PDFController(IPDFService pdfService)
    {
        _pdfService = pdfService;
    }
    
    [HttpPost]
    public async Task<ActionResult> ImageTOPdf(IFormFile file)
    {
        try
        {
            
            var name = file.ContentType.Split("/");
            Debug.WriteLine(name[1]);
            var length = name[1].Length + 1;
            var fileName = file.FileName.Remove(file.FileName.Length - length);
            var blobName = DateTime.UtcNow.ToString("s")+"_"+fileName;
            
           _pdfService.SplitPDF(file.FileName, blobName+".pdf",file.OpenReadStream());

           // email track***********
           //return File(result, "application/pdf", blobName);
           return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPost("fetch-email")]
    public async Task<ActionResult> FetchEmail([FromHeader] HeaderModel model, [FromBody] ProfileDTO profileDto)
    {
        var mod = model;
        var mods = profileDto;
        /*if(data["fullname"].ToString().Length == 0)
            Debug.WriteLine("hii");*/
       
        return Ok();
    }
}