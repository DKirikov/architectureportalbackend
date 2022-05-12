using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ArchitecturePortalBackend.DataAccess.DI;
using ArchitecturePortalBackend.Migrator.Services;
using ArchitecturePortalBackend.Migrator.Settings;
using ArchitecturePortalBackend.VaultProvider;

const string DATABASE_KEY = "AzureSQL_ArcPortalDb_mgt";

var secretName = "infra-ArchitecturePortalBackend";
var secretFileName = "configs/" + secretName;
#if DEBUG
    VaultProvider.WriteSecretToFile(secretName, secretFileName);
#endif

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(hostConfig =>
    {
        hostConfig.AddJsonFile(secretFileName, false, true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Migrator>()
            .Configure<MigratorSettings>(hostContext.Configuration.GetSection(nameof(MigratorSettings)))
            .AddDataAccessServices(hostContext.Configuration.GetConnectionString(DATABASE_KEY), Assembly.GetExecutingAssembly().GetName().Name);
    });

await builder.Build().RunAsync().ConfigureAwait(false);