using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record ContractRm : EntityIdRm
{
    [Required]
    public ContractType ContractType { get; set; }
    [Required]
    public IEnumerable<Guid> ClientServices { get; set; } = null!;
    public Guid? ServiceId { get; set; }
    public string? LinkToContract { get; set; }
}