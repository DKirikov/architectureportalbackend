using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class ContractsService : BaseService, IContractsService
{
    private readonly IMapper _mapper;
    private readonly IDataAccessor _dataAccessor;
    public ContractsService(IMapper mapper, IDataAccessor dataAccessor)
    {
        _mapper = mapper;
        _dataAccessor = dataAccessor;
    }

    public async Task<IEnumerable<ContractRm>> GetContractsAsync()
    {
        return await _dataAccessor.Contracts
            .Include(c => c.Service)
            .Include(s => s.ClientServices)
            .Select(s => _mapper.Map<ContractRm>(s))
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<ContractDetailsRm> GetContractsAsync(Guid id)
    {
        var existingContract = await GetExistingContractAsync(id, true).ConfigureAwait(false);

        return _mapper.Map<ContractDetailsRm>(existingContract);
    }

    public async Task<EntityIdRm> CreateContractAsync(ContractWm contractWm)
    {
        var newContract = _mapper.Map<Contract>(contractWm);
        newContract.Id = Guid.NewGuid();

        _dataAccessor.Contracts.Add(newContract);
        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);

        var res = _mapper.Map<EntityIdRm>(newContract.Id);
        return res;
    }

    public async Task UpdateContractAsync(Guid id, ContractWm contractWm)
    {
        var existingContract = await GetExistingContractAsync(id, false).ConfigureAwait(false);

        _mapper.Map(contractWm, existingContract);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteContractAsync(Guid id)
    {
        var existingContract = await GetExistingContractAsync(id, false).ConfigureAwait(false);

        _dataAccessor.Contracts.Remove(existingContract);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task<Contract> GetExistingContractAsync(Guid id, bool includeSubEntities)
    {
        var existingContractQuery = _dataAccessor.Contracts.Where(c => c.Id == id);
        if (includeSubEntities)
        {
            existingContractQuery = existingContractQuery.Include(c => c.Service).Include(c => c.ClientServices);
        }

        return await GetExistingEntityAsync(id, existingContractQuery);
    }
}
