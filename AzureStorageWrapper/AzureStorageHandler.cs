using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Sas;
using AzureStorageWrapper.Models;
using AzureStorageWrapper.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AzureSecretsManager.Models;

namespace AzureStorageWrapper;

public class AzureStorageHandler : IAzureStorageHandler
{
    private readonly ShareDirectoryClient _directoryClient;

    public AzureStorageHandler(IOptions<AzureStorageKeys> azureStorage, IOptions<AzureSqlKeys> azureSql)
    {
        Console.WriteLine(azureStorage.Value.ConnectionString);
        Console.WriteLine(azureSql.Value.ConnectionString);
        var serviceClient = new ShareServiceClient(azureStorage.Value.ConnectionString);
        var shareClient = serviceClient.GetShareClient(azureStorage.Value.ShareName);
        _directoryClient = shareClient.GetDirectoryClient(azureStorage.Value.Directory);
    }
    
    public int UploadFile(IFormFile file)
    {
        var fileClient = _directoryClient.GetFileClient(file.FileName);
        fileClient.Create(file.Length);
        Response<ShareFileUploadInfo>? res = fileClient.UploadRange(new HttpRange(0, file.Length), file.OpenReadStream());
        return res.GetRawResponse().Status;
    }
    
    public async Task<IList<AzureFile>> GetFiles()
    {
        
        IList<AzureFile> files = new List<AzureFile>();
        await foreach (var item in _directoryClient.GetFilesAndDirectoriesAsync())
        {
            files.Add(new AzureFile
            {
                Name = item.Name,
                Type = item.IsDirectory ? "Directory" : "File",
                Size = item.FileSize.ToString()
            });
        }
        
        return files;
    }
    
    public string GetFile(string name)
    {
        var fileClient = _directoryClient.GetFileClient(name);
        var fileDownload = fileClient.Download().Value;
        using var reader = new StreamReader(fileDownload.Content);
        var file = reader.ReadToEnd();
        
        return file;
    }
    
    public async Task<Stream> GetImage(string name)
    {
        var fileClient = _directoryClient.GetFileClient(name);
    
        if (!await fileClient.ExistsAsync())
        {
            return NotFound();
        }
    
        var download = await fileClient.DownloadAsync();
        return download.Value.Content;
    }
    
    public Uri GetSasUri(string name)
    {
        var fileClient = _directoryClient.GetFileClient(name);
        return fileClient.GenerateSasUri(ShareFileSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1));
    }

    public int RemoveFile(string fileName)
    {
        var fileClient = _directoryClient.GetFileClient(fileName);
        var res = fileClient.Delete();
        Console.WriteLine(res.Status);
        return res.Status;
    }

    private static Stream NotFound()
    {
        throw new NotImplementedException();
    }
}