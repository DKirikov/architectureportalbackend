using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace ArchitecturePortalBackend.VaultProvider;

public static class VaultProvider
{
    private const string VaultUri = "https://infra-keyvault01.vault.azure.net/";
    public static void WriteSecretToFile(string secretName, string secretFileName)
    {
        var architecturePortalBackendSecret = GetValueFromKeyVault(secretName);

        var directoryName = Path.GetDirectoryName(secretFileName);

        if (!Directory.Exists(Directory.GetCurrentDirectory() + "/" + directoryName))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + directoryName);
        }

        File.WriteAllText(Directory.GetCurrentDirectory() + "/" + secretFileName, architecturePortalBackendSecret);
    }

    private static string GetValueFromKeyVault(string secretName)
    {
        var azureServiceTokenProvider = new AzureServiceTokenProvider();
        var authenticationCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
        var keyVaultClient = new KeyVaultClient(authenticationCallback);

        var secret = keyVaultClient.GetSecretAsync(VaultUri, secretName).ConfigureAwait(false).GetAwaiter().GetResult();
        return secret.Value;
    }
}