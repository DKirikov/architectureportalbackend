using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;

public record ServiceDetailsRm : ServiceRm, IDateTimeRm
{
    [Required]
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? Description { get; set; }
    public string? GitLabRepositoryUrl { get; set; }
}