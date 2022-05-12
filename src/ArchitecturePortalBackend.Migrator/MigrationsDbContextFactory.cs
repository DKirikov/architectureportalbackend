using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ArchitecturePortalBackend.DataAccess;
using ArchitecturePortalBackend.Migrator.Helpers;

namespace ArchitecturePortalBackend.Migrator;

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<ArcPortalDbContext>
{
    private const string MIGRATON_CONNECTION_STRING = "FakeConnectionString";
    public ArcPortalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ArcPortalDbContext>();
            
        optionsBuilder.UseSqlServer(MIGRATON_CONNECTION_STRING, providerOptions => providerOptions.MigrationsAssembly(MigrationsAssemblyHelper.AssemblyName));

        return new ArcPortalDbContext(optionsBuilder.Options);
    }
}