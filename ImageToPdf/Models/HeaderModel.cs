using Microsoft.AspNetCore.Mvc;

namespace ImageToPdf.Models;

public class HeaderModel
{
    [FromHeader]
    public int Company { get; set; }
    [FromHeader]
    public int Client { get; set; }
    [FromHeader]
    public int User { get; set; }
}