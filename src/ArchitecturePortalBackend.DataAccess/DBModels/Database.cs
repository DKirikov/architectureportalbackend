using ArchitecturePortalBackend.DataAccess.DBModels.Enums;

namespace ArchitecturePortalBackend.DataAccess.DBModels;

public record Database : BaseEntity
{
    public DbType DbType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Service? Service { get; set; }
    public Guid? ServiceId { get; set; }
}
