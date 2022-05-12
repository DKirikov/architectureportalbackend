using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.DataAccess.DBModels;

namespace ArchitecturePortalBackend.DataAccess.Interfaces;

public interface IDataAccessor
{
    public DbSet<Service> Services { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Database> Databases { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}