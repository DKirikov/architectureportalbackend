namespace ArchitecturePortalBackend.BusinessLogic.Interfaces;

public interface IContractsManagementService
{
    public Task MarkContractAsUsed(Guid contractId, Guid serviceId);
    public Task UnmarkContractAsUsed(Guid contractId, Guid serviceId);
}
