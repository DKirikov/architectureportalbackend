using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.DataAccess.DI;

public static class DataAccessServicesRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString, string? migrationAssemblyName = null)
    {
        services.AddDbContextPool<IDataAccessor, ArcPortalDbContext>((provider, options) =>
        {
            options.ConfigureWarnings(t =>
            {
                t.Log(CoreEventId.SensitiveDataLoggingEnabledWarning);
                t.Throw();
            });

            if (migrationAssemblyName == null)
            {
                options.UseSqlServer(connectionString!);
            }
            else
            {
                options.UseSqlServer(connectionString!, builder => builder.MigrationsAssembly(migrationAssemblyName));
            }
        });

        return services;
    }
}