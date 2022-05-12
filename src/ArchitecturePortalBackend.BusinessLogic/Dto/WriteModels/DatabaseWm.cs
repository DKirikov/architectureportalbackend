using System.ComponentModel.DataAnnotations;
using ArchitecturePortalBackend.BusinessLogic.Dto.Enums;

namespace ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

public record DatabaseWm
{
    [Required]
    [EnumDataType(typeof(DbType))]
    public DbType? DbType { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ServiceId { get; set; }
}