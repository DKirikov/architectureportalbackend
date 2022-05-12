using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;

namespace ArchitecturePortalBackend.BusinessLogic.Interfaces;

public interface IServicesService
{
    Task<IEnumerable<ServiceRm>> GetServicesAsync();
    Task<ServiceDetailsRm> GetServicesAsync(Guid id);
    Task<EntityIdRm> CreateServiceAsync(ServiceWm serviceWm);
    Task UpdateServiceAsync(Guid id, ServiceWm serviceWm);
    Task DeleteServiceAsync(Guid id);
}