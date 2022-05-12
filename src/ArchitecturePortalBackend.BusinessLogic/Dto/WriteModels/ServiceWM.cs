using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

public record ServiceWm
{
    [Required]
    public string? Name { get; set; }
    [Required]
    [EnumDataType(typeof(ProjectStatus))]
    public ProjectStatus? ProjectStatus { get; set; }
    public string? Description { get; set; }
    public Guid? ModuleId { get; set; }
    public string? GitLabRepositoryUrl { get; set; }
}