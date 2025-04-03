using System.Text.Json.Serialization;

namespace AzureStorageWrapper.Models;

public class AzureFile
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("size")]
    public string? Size { get; set; }
    
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}