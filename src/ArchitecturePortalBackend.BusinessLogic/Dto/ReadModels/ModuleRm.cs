using System.ComponentModel.DataAnnotations;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record ModuleRm : EntityIdRm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public IEnumerable<Guid> Services { get; set; } = null!;
}