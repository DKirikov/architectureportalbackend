using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record DatabaseRm : EntityIdRm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public DbType DbType { get; set; }
    public Guid? ServiceId { get; set; }
}