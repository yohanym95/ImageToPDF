using System.ComponentModel.DataAnnotations;

namespace ImageToPdf.Models;

public class ProfileDTO
{
    [Required]
    public string FullName { get; set; }

    public string Email { get; set; } = "";

}