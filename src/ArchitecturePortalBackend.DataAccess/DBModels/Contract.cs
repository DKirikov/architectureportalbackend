using ArchitecturePortalBackend.DataAccess.DBModels.Enums;

namespace ArchitecturePortalBackend.DataAccess.DBModels;

public record Contract : BaseEntity
{
    public ContractType ContractType { get; set; }
    public string? LinkToContract { get; set; }
    public string? Description { get; set; }
    public Service? Service { get; set; }
    public Guid? ServiceId { get; set; }
    public ICollection<Service> ClientServices { get; set; } = null!;
}