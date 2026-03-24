using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.Entities;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Account, AccountDTO>().ReverseMap();
        CreateMap<Users, UserDTO>().ReverseMap();
        CreateMap<Roles, RolesDTO>().ReverseMap();
        CreateMap<Permissions, PermissionsDTO>().ReverseMap();
        CreateMap<Patients, PatientsDTO>().ReverseMap();

        CreateMap<Doctors, DoctorsDTO>().ReverseMap();

        CreateMap<Departments, DepartmentsDTO>().ReverseMap();

        CreateMap<Appointments, AppointmentsDTO>().ReverseMap();
    }
}