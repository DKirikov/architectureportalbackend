using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

namespace ArchitecturePortalBackend.BusinessLogic.Interfaces;

public interface IDatabaseService
{
    Task<IEnumerable<DatabaseRm>> GetDatabasesAsync();
    Task<DatabaseDetailsRm> GetDatabasesAsync(Guid id);
    Task<EntityIdRm> CreateDatabaseAsync(DatabaseWm databaseWm);
    Task UpdateDatabaseAsync(Guid id, DatabaseWm databaseWm);
    Task DeleteDatabaseAsync(Guid id);
}