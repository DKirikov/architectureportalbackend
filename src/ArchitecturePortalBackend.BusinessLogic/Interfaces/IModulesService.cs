using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

namespace ArchitecturePortalBackend.BusinessLogic.Interfaces;

public interface IModulesService
{
    Task<IEnumerable<ModuleRm>> GetModulesAsync();
    Task<ModuleDetailsRm> GetModulesAsync(Guid id);
    Task<EntityIdRm> CreateModuleAsync(ModuleWm moduleWm);
    Task UpdateModuleAsync(Guid id, ModuleWm moduleWm);
    Task DeleteModuleAsync(Guid id);
}