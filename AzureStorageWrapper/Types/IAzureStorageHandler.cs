using AzureStorageWrapper.Models;
using Microsoft.AspNetCore.Http;

namespace AzureStorageWrapper.Types;

public interface IAzureStorageHandler
{
    int UploadFile(IFormFile file);
    Task<IList<AzureFile>> GetFiles();
    string GetFile(string name);
    Task<Stream> GetImage(string name);
    
    Uri GetSasUri(string name);
    int RemoveFile(string fileName);
}