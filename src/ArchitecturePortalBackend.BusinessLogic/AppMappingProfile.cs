using AutoMapper;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.DataAccess.DBModels;

namespace ArchitecturePortalBackend.BusinessLogic;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ModuleWm, Module>();
        CreateMap<Module, ModuleRm>()
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services!.Select(s => s.Id)));
        CreateMap<Module, ModuleDetailsRm>()
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services!.Select(s => s.Id)));

        CreateMap<ServiceWm, Service>();
        CreateMap<Service, ServiceRm>()
            .ForMember(dest => dest.ProvidedContracts, opt => opt.MapFrom(src => src.ProvidedContracts.Select(s => s.Id)))
            .ForMember(dest => dest.UsedContracts, opt => opt.MapFrom(src => src.UsedContracts.Select(s => s.Id)))
            .ForMember(dest => dest.Databases, opt => opt.MapFrom(src => src.Databases.Select(s => s.Id)))
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module == null ? (Guid?)null : src.Module.Id));
        CreateMap<Service, ServiceDetailsRm>()
            .ForMember(dest => dest.ProvidedContracts, opt => opt.MapFrom(src => src.ProvidedContracts.Select(s => s.Id)))
            .ForMember(dest => dest.UsedContracts, opt => opt.MapFrom(src => src.UsedContracts.Select(s => s.Id)))
            .ForMember(dest => dest.Databases, opt => opt.MapFrom(src => src.Databases.Select(s => s.Id)))
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module == null ? (Guid?)null : src.Module.Id));

        CreateMap<ContractWm, Contract>();
        CreateMap<Contract, ContractRm>()
            .ForMember(dest => dest.ClientServices, opt => opt.MapFrom(src => src.ClientServices.Select(s => s.Id)))
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.Service == null ? (Guid?) null :src.Service.Id));
        CreateMap<Contract, ContractDetailsRm>()
            .ForMember(dest => dest.ClientServices, opt => opt.MapFrom(src => src.ClientServices.Select(s => s.Id)))
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.Service == null ? (Guid?)null : src.Service.Id));

        CreateMap<DatabaseWm, Database>();
        CreateMap<Database, DatabaseRm>()
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.Service == null ? (Guid?)null : src.Service.Id));
        CreateMap<Database, DatabaseDetailsRm>()
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.Service == null ? (Guid?)null : src.Service.Id));

        CreateMap<Guid, EntityIdRm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}