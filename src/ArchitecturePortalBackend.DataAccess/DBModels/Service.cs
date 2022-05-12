using ArchitecturePortalBackend.DataAccess.DBModels.Enums;

namespace ArchitecturePortalBackend.DataAccess.DBModels;

public record Service : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    public Module? Module { get; set; }
    public Guid? ModuleId { get; set; }
    public string? GitLabRepositoryUrl { get; set; }
    public IEnumerable<Database> Databases { get; set; } = null!;
    public IEnumerable<Contract> ProvidedContracts { get; set; } = null!;
    public ICollection<Contract> UsedContracts { get; set; } = null!;
}