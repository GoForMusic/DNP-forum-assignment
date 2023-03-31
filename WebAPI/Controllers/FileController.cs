using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController:ControllerBase
{
    public IWebHostEnvironment webHostEnvironment;

    public FileController(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }
 
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] FileUpload objFileUpload)
    {
        try
        {
            if (objFileUpload.files.Length > 0)
            {
                string path = webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                string ext = Path.GetExtension(objFileUpload.files.FileName);
                if (ext.ToLower() == ".docx" || ext.ToLower() == ".png")
                {
                    using (FileStream fileStream = System.IO.File.Create(path + objFileUpload.files.FileName))
                    {
                        Console.WriteLine(objFileUpload.files.FileName);
                        await objFileUpload.files.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return StatusCode(200, "Uploaded");
                    }
                }
                else
                {
                    return StatusCode(415, $"Files \"{ext.ToLower()}\" not allowed!");
                }
                
            }
            else
            {
                return StatusCode(409, "Not Uploaded.");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}