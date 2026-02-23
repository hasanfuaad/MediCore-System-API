using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Domain.Entites;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Account, AccountDTO>().ReverseMap();
        CreateMap<Users, UserDTO>().ReverseMap();
        CreateMap<Roles, RolesDTO>().ReverseMap();
        CreateMap<Permissions, PermissionsDTO>().ReverseMap();
    }
}
