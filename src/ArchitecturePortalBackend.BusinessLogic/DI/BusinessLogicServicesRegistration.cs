using Microsoft.Extensions.DependencyInjection;
using ArchitecturePortalBackend.BusinessLogic.Implementation;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.BusinessLogic.DI;

public static class BusinessLogicServicesRegistration
{
    public static IServiceCollection AddBusinessLogicServices(
        this IServiceCollection services)
    {
        services.AddScoped<IModulesService, ModulesService>();
        services.AddScoped<IServicesService, ServicesService>();
        services.AddScoped<IContractsService, ContractsService>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<IContractsManagementService, ContractsManagementService>();
        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}