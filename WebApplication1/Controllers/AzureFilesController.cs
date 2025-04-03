using Microsoft.AspNetCore.Mvc;
using AzureStorageWrapper.Models;
using AzureStorageWrapper.Types;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class AzureFilesController(IAzureStorageHandler azureStorageHandler) : ControllerBase
{
    private IAzureStorageHandler AzureStorageHandler { get; } = azureStorageHandler;

    [HttpPost("AddFile")]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest("Invalid file");
        }
        return AzureStorageHandler.UploadFile(file) == 201 ? Ok("File uploaded") : BadRequest("Error uploading file");
    }
    
    [HttpGet("GetFiles")]
    public IEnumerable<AzureFile> GetFiles()
    {
        return AzureStorageHandler.GetFiles().Result;
    }
    
    [HttpGet("GetFileContent")]
    public string GetFileContent(string name)
    {
        var file =  AzureStorageHandler.GetFile(name);
        Console.WriteLine(file);
        return file;
    }
    
    [HttpGet("image/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var file = AzureStorageHandler.GetImage(fileName);
        return File(file.Result, "image/jpeg", fileName);
    }
    
    [HttpGet("sas/{fileName}")]
    public Uri GetSasImage(string fileName)
    {
        var file = AzureStorageHandler.GetSasUri(fileName);
        return file;
    }
    
    [HttpDelete("remove/{fileName}")]
    public IActionResult RemoveFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return BadRequest("Invalid file name");
        }
        return AzureStorageHandler.RemoveFile(fileName) == 202 ? Ok("File removed") : BadRequest("Error removing file");
    }   
}