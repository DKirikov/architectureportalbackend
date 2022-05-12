using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class ServicesService : BaseService, IServicesService
{
    private readonly IMapper _mapper;
    private readonly IDataAccessor _dataAccessor;
    public ServicesService(IMapper mapper, IDataAccessor dataAccessor)
    {
        _mapper = mapper;
        _dataAccessor = dataAccessor;
    }

    public async Task<IEnumerable<ServiceRm>> GetServicesAsync()
    {
        return await _dataAccessor.Services.Include(s => s.UsedContracts)
            .Include(s => s.Module)
            .Include(s => s.ProvidedContracts)
            .Include(s => s.UsedContracts)
            .Include(s => s.Databases)
            .Select(s => _mapper.Map<ServiceRm>(s))
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<ServiceDetailsRm> GetServicesAsync(Guid id)
    {
        var existingService = await GetExistingServiceAsync(id, true).ConfigureAwait(false);

        return _mapper.Map<ServiceDetailsRm>(existingService);
    }

    public async Task<EntityIdRm> CreateServiceAsync(ServiceWm serviceWm)
    {
        var newService = _mapper.Map<Service>(serviceWm);
        newService.Id = Guid.NewGuid();
        
        _dataAccessor.Services.Add(newService);
        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);

        var res = _mapper.Map<EntityIdRm>(newService.Id);
        return res;
    }

    public async Task UpdateServiceAsync(Guid id, ServiceWm serviceWm)
    {
        var existingService = await GetExistingServiceAsync(id, false).ConfigureAwait(false);

        _mapper.Map(serviceWm, existingService);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteServiceAsync(Guid id)
    {
        var existingService = await GetExistingServiceAsync(id, false).ConfigureAwait(false);

        _dataAccessor.Services.Remove(existingService);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task<Service> GetExistingServiceAsync(Guid id, bool includeSubEntities)
    {
        var existingServiceQuery = _dataAccessor.Services.Where(s => s.Id == id);
        if (includeSubEntities)
        {
            existingServiceQuery = existingServiceQuery.Include(s => s.Module).Include(s => s.ProvidedContracts).Include(s => s.UsedContracts).Include(s => s.Databases);
        }

        return await GetExistingEntityAsync(id, existingServiceQuery);
    }
}