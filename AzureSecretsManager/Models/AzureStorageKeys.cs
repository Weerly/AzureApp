namespace AzureSecretsManager.Models;

public class AzureStorageKeys
{ 
    public string? ConnectionString {get; set;}
    public string? ContainerName {get; set;}
    public string? ShareName {get; set;}
    public string? Directory {get; set;}
}