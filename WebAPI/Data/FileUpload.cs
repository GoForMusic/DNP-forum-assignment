using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data;

public class FileUpload
{
    [Required]
    public IFormFile files { get; set; }
}