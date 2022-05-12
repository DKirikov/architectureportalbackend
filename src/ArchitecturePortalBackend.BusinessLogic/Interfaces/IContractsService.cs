using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

namespace ArchitecturePortalBackend.BusinessLogic.Interfaces;

public interface IContractsService
{
    Task<IEnumerable<ContractRm>> GetContractsAsync();
    Task<ContractDetailsRm> GetContractsAsync(Guid id);
    Task<EntityIdRm> CreateContractAsync(ContractWm contractWm);
    Task UpdateContractAsync(Guid id, ContractWm contractWm);
    Task DeleteContractAsync(Guid id);
}