namespace ArchitecturePortalBackend.Migrator.Helpers;

public static class MigrationsAssemblyHelper
{
    public static readonly string? AssemblyName = typeof(MigrationsAssemblyHelper).Assembly.GetName().Name;
}