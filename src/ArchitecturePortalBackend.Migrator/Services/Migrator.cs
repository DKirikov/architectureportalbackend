using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ArchitecturePortalBackend.DataAccess;
using ArchitecturePortalBackend.Migrator.Settings;

namespace ArchitecturePortalBackend.Migrator.Services;

public class Migrator : BackgroundService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Migrator> _logger;
    private readonly IOptionsMonitor<MigratorSettings> _settings;

    public Migrator(IHostApplicationLifetime applicationLifetime,
        IServiceProvider serviceProvider,
        IOptionsMonitor<MigratorSettings> settings,
        ILogger<Migrator> logger)
    {
        _applicationLifetime = applicationLifetime;

        _serviceProvider = serviceProvider;
        _settings = settings;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await MigrateDatabaseAsync(stoppingToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Migration failed with error.");
            Environment.ExitCode = 1;
        }
        finally
        {
            _applicationLifetime.StopApplication();
        }
    }

    private async Task MigrateDatabaseAsync(CancellationToken token)
    {
        using var scope = _serviceProvider.CreateScope(); // this will use `IServiceScopeFactory` internally
        var context = scope.ServiceProvider.GetService<ArcPortalDbContext>();
        var migrator = context!.GetService<IMigrator>();

        _logger.LogInformation($"Opening DB connection to {context!.Database.GetDbConnection().Database}.");
        await context.Database.OpenConnectionAsync(token).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(_settings.CurrentValue.TargetMigration) || _settings.CurrentValue.TargetMigration == "latest")
        {
            _logger.LogInformation("Migrating database to latest version.");
            await migrator.MigrateAsync(cancellationToken: token).ConfigureAwait(false);
        }
        else
        {
            _logger.LogInformation($"Migrating database to {_settings.CurrentValue.TargetMigration} version.");
            await migrator.MigrateAsync(_settings.CurrentValue.TargetMigration, token).ConfigureAwait(false);
        }

        await context.SaveChangesAsync(token).ConfigureAwait(false);

        await context.Database.CloseConnectionAsync().ConfigureAwait(false);
        _logger.LogInformation("Database migration done.");
    }
}