using Azure.Identity;

namespace AzureSecretsManager;

public static class SecretGrabber
{
    public static Uri GetKeyVaultUrl(string keyVaultName)
    {
        return new Uri($"https://{keyVaultName}.vault.azure.net/");
    }

    public  static DefaultAzureCredential GetCredentials()
    {
        return new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ExcludeEnvironmentCredential = true,
            ExcludeManagedIdentityCredential = true,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeVisualStudioCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            ExcludeAzurePowerShellCredential = true,
            ExcludeAzureDeveloperCliCredential = true,
            ExcludeAzureCliCredential = false
        });
    }
}