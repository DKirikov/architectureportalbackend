using System.ComponentModel.DataAnnotations;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

public record ModuleWm
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
}