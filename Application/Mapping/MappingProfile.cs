using Application.Cache.Entities;
using Application.Client.DTO;
using Version = System.Version;

namespace Application.Mapping;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RoomDto, Room>();
        CreateMap<TeacherDto, Teacher>();
        CreateMap<VersionDto, Version>();
    }
}