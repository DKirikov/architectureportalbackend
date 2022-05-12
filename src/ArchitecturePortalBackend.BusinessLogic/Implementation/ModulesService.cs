using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class ModulesService : BaseService, IModulesService
{
    private readonly IMapper _mapper;
    private readonly IDataAccessor _dataAccessor;
    public ModulesService(IMapper mapper, IDataAccessor dataAccessor)
    {
        _mapper = mapper;
        _dataAccessor = dataAccessor;
    }

    public async Task<IEnumerable<ModuleRm>> GetModulesAsync()
    {
        return await _dataAccessor.Modules.Include(m => m.Services).Select(s => _mapper.Map<ModuleRm>(s)).ToListAsync().ConfigureAwait(false);
    }

    public async Task<ModuleDetailsRm> GetModulesAsync(Guid id)
    {
        var existingModule = await GetExistingModuleAsync(id, true).ConfigureAwait(false);

        return _mapper.Map<ModuleDetailsRm>(existingModule);
    }

    public async Task<EntityIdRm> CreateModuleAsync(ModuleWm moduleWm)
    {
        var newModule = _mapper.Map<Module>(moduleWm);
        newModule.Id = Guid.NewGuid();

        _dataAccessor.Modules.Add(newModule);
        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);

        var res = _mapper.Map<EntityIdRm>(newModule.Id);
        return res;
    }

    public async Task UpdateModuleAsync(Guid id, ModuleWm moduleWm)
    {
        var existingModule = await GetExistingModuleAsync(id, false).ConfigureAwait(false);

        _mapper.Map(moduleWm, existingModule);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteModuleAsync(Guid id)
    {
        var existingModule = await GetExistingModuleAsync(id, false).ConfigureAwait(false);

        _dataAccessor.Modules.Remove(existingModule);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task<Module> GetExistingModuleAsync(Guid id, bool includeSubEntities)
    {
        var existingModuleQuery = _dataAccessor.Modules.Where(m => m.Id == id);
        if (includeSubEntities)
        {
            existingModuleQuery = existingModuleQuery.Include(m => m.Services);
        }

        return await GetExistingEntityAsync(id, existingModuleQuery);
    }
}