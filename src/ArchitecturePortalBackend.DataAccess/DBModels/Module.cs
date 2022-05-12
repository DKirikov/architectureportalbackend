namespace ArchitecturePortalBackend.DataAccess.DBModels;

public record Module : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Service> Services { get; set; } = null!;
}