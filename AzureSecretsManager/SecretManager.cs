namespace AzureSecretsManager;

public static class SecretManager
{
    public static void ConfigureSecrets( WebApplicationBuilder builder, string keyVaultName)
    {
        builder.Configuration
            .AddAzureKeyVault(SecretGrabber.GetKeyVaultUrl(keyVaultName), SecretGrabber.GetCredentials());
    }
}
