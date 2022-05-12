namespace ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels.Interfaces;

internal interface IDateTimeRm
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}