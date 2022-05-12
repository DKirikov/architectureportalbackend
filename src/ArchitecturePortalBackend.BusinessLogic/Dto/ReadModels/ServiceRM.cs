using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record ServiceRm : EntityIdRm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public ProjectStatus ProjectStatus { get; set; }
    public Guid? ModuleId { get; set; }
    [Required]
    public IEnumerable<Guid> ProvidedContracts { get; set; } = null!;
    [Required]
    public IEnumerable<Guid> UsedContracts { get; set; } = null!;
    [Required]
    public IEnumerable<Guid> Databases { get; set; } = null!;
}