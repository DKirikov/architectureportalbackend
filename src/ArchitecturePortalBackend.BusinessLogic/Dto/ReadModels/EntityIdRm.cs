using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record EntityIdRm : IEntityIdRm
{
    [Required]
    public Guid Id { get; set; }
}