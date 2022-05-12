using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class ContractsManagementService : BaseService, IContractsManagementService
{
    private readonly IDataAccessor _dataAccessor;
    public ContractsManagementService(IDataAccessor dataAccessor)
    {
        _dataAccessor = dataAccessor;
    }

    public async Task MarkContractAsUsed(Guid contractId, Guid serviceId)
    {
        var existingContract = await GetExistingContractAsync(contractId).ConfigureAwait(false);
        var existingService = await GetExistingServiceAsync(serviceId).ConfigureAwait(false);

        if (!existingContract.ClientServices.Select(s => s.Id).ToList().Contains(serviceId))
        {
            existingContract.ClientServices.Add(existingService);

            await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task UnmarkContractAsUsed(Guid contractId, Guid serviceId)
    {
        var existingContract = await GetExistingContractAsync(contractId).ConfigureAwait(false);
        var existingService = await GetExistingServiceAsync(serviceId).ConfigureAwait(false);

        if (existingContract.ClientServices.Select(s => s.Id).ToList().Contains(serviceId))
        {
            existingContract.ClientServices.Remove(existingService);

            await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    private async Task<Contract> GetExistingContractAsync(Guid id)
    {
        var existingContractQuery = _dataAccessor.Contracts.Where(m => m.Id == id).Include(m => m.ClientServices);

        return await GetExistingEntityAsync(id, existingContractQuery);
    }

    private async Task<Service> GetExistingServiceAsync(Guid id)
    {
        var existingServiceQuery = _dataAccessor.Services.Where(s => s.Id == id);

        return await GetExistingEntityAsync(id, existingServiceQuery);
    }
}
