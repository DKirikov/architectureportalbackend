using Microsoft.EntityFrameworkCore;
using ArchitecturePortalBackend.BusinessLogic.Exceptions;

namespace ArchitecturePortalBackend.BusinessLogic.Implementation;

public class BaseService
{
    protected async Task<T> GetExistingEntityAsync<T>(Guid id, IQueryable<T> existingEntityQuery)
    {
        var existingService = await existingEntityQuery.FirstOrDefaultAsync().ConfigureAwait(false);
        if (existingService == null)
        {
            throw new NotFoundException($"{typeof(T).Name} with id '{id}' not found in DB");
        }

        return existingService;
    }
}
