using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;
using ArchitecturePortalBackend.DataAccess.DBModels;
using ArchitecturePortalBackend.DataAccess.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class DatabaseService : BaseService, IDatabaseService
{
    private readonly IMapper _mapper;
    private readonly IDataAccessor _dataAccessor;
    public DatabaseService(IMapper mapper, IDataAccessor dataAccessor)
    {
        _mapper = mapper;
        _dataAccessor = dataAccessor;
    }

    public async Task<IEnumerable<DatabaseRm>> GetDatabasesAsync()
    {
        return await _dataAccessor.Databases
            .Include(s => s.Service)
            .Select(s => _mapper.Map<DatabaseRm>(s))
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<DatabaseDetailsRm> GetDatabasesAsync(Guid id)
    {
        var existingDatabase = await GetExistingDatabaseAsync(id, true).ConfigureAwait(false);

        return _mapper.Map<DatabaseDetailsRm>(existingDatabase);
    }

    public async Task<EntityIdRm> CreateDatabaseAsync(DatabaseWm databaseWm)
    {
        var newDatabase = _mapper.Map<Database>(databaseWm);
        newDatabase.Id = Guid.NewGuid();

        _dataAccessor.Databases.Add(newDatabase);
        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);

        var res = _mapper.Map<EntityIdRm>(newDatabase.Id);
        return res;
    }

    public async Task UpdateDatabaseAsync(Guid id, DatabaseWm databaseWm)
    {
        var existingDatabase = await GetExistingDatabaseAsync(id, false).ConfigureAwait(false);

        _mapper.Map(databaseWm, existingDatabase);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteDatabaseAsync(Guid id)
    {
        var existingDatabase = await GetExistingDatabaseAsync(id, false).ConfigureAwait(false);

        _dataAccessor.Databases.Remove(existingDatabase);

        await _dataAccessor.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task<Database> GetExistingDatabaseAsync(Guid id, bool includeSubEntities)
    {
        var existingDatabaseQuery = _dataAccessor.Databases.Where(m => m.Id == id);
        if (includeSubEntities)
        {
            existingDatabaseQuery = existingDatabaseQuery.Include(s => s.Service);
        }

        return await GetExistingEntityAsync(id, existingDatabaseQuery);
    }
}