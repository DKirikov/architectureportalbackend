using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.DBModels.Enums;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.DataAccess;

public class ArcPortalDbContext : DbContext, IDataAccessor
{
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<Database> Databases { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;

    public ArcPortalDbContext(DbContextOptions<ArcPortalDbContext> options)
        : base(options)
    {
        //Database.EnsureDeleted(); //doesn't work. Use script ClearDBSchema.sql
        //Database.EnsureCreated();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateEntityFields();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default
    )
    {
        UpdateEntityFields();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureModules(modelBuilder.Entity<Module>());
        ConfigureServices(modelBuilder.Entity<Service>());
        ConfigureDatabases(modelBuilder.Entity<Database>());
        ConfigureContracts(modelBuilder.Entity<Contract>());

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureBaseEntity<T>(EntityTypeBuilder<T> entity) where T : BaseEntity
    {
        entity.HasQueryFilter(m => !m.IsDeleted);
    }

    private void ConfigureModules(EntityTypeBuilder<Module> entity)
    {
        ConfigureBaseEntity(entity);
    }

    private void ConfigureServices(EntityTypeBuilder<Service> entity)
    {
        ConfigureBaseEntity(entity);

        entity.Property(s => s.ProjectStatus)
            .HasConversion(
                v => v.ToString(),
                v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v));

        entity.Property(s => s.ModuleId)
            .IsRequired(false);

        entity
            .HasOne(s => s.Module)
            .WithMany(m => m.Services)
            .HasForeignKey(s => s.ModuleId);
    }

    private void ConfigureDatabases(EntityTypeBuilder<Database> entity)
    {
        ConfigureBaseEntity(entity);

        entity.Property(s => s.DbType)
            .HasConversion(
                v => v.ToString(),
                v => (DbType)Enum.Parse(typeof(DbType), v));

        entity.Property(s => s.ServiceId)
            .IsRequired(false);

        entity
            .HasOne(db => db.Service)
            .WithMany(s => s.Databases)
            .HasForeignKey(db => db.ServiceId);
    }

    private void ConfigureContracts(EntityTypeBuilder<Contract> entity)
    {
        ConfigureBaseEntity(entity);

        entity.Property(s => s.ContractType)
            .HasConversion(
                v => v.ToString(),
                v => (ContractType)Enum.Parse(typeof(ContractType), v));

        entity.Property(s => s.ServiceId)
            .IsRequired(false);

        entity
            .HasOne(c => c.Service)
            .WithMany(s => s.ProvidedContracts)
            .HasForeignKey(c => c.ServiceId);

        entity
            .HasMany(c => c.ClientServices)
            .WithMany(s => s.UsedContracts)
            .UsingEntity(j => j.ToTable("Interactions"));
    }

    private void UpdateEntityFields()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is BaseEntity))
        {
            var entity = entry.Entity as BaseEntity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity!.CreatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity!.UpdatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entity!.IsDeleted = true;
                    entity.UpdatedOn = DateTime.UtcNow;
                    break;
            }
        }
    }
}