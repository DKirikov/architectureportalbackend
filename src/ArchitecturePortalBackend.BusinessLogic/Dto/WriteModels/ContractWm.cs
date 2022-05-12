using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

public record ContractWm
{
    [Required]
    [EnumDataType(typeof(ContractType))]
    public ContractType? ContractType { get; set; }
    public string? LinkToContract { get; set; }
    public string? Description { get; set; }
    public Guid? ServiceId { get; set; }
}