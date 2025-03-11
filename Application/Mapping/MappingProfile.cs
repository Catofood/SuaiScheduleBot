namespace Application.Mapping;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client.DTO.Building, DB.Entity.Building>();
        CreateMap<Client.DTO.Department, DB.Entity.Department>();
        CreateMap<Client.DTO.Event, DB.Entity.Event>();
        CreateMap<Client.DTO.Group, DB.Entity.Group>();
        CreateMap<Client.DTO.Room, DB.Entity.Room>();
        CreateMap<Client.DTO.Teacher, DB.Entity.Teacher>();
        CreateMap<Client.DTO.Version, DB.Entity.Version>();
    }
}