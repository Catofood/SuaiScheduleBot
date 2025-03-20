using Application.Cache.Entities;
using Application.Client.DTO;
using Version = Application.Cache.Entities.Version;

namespace Application.Mapping;

using AutoMapper;

public class DtoToCacheEntitiesMapping : Profile
{
    public DtoToCacheEntitiesMapping()
    {
        CreateMap<List<BuildingDto>, Dictionary<long, string>>()
            .ConvertUsing(src => src
                .ToDictionary(
                building => building.ItemId, 
                building => building.Name));
        
        CreateMap<List<RoomDto>, Dictionary<long, Room>>()
            .ConvertUsing(src => src
                .ToDictionary(
                    room => room.ItemId, 
                    room => new Room()
                    {
                        Name = room.Name, 
                        BuildingId = room.BuildingId
                    }));
        
        CreateMap<List<GroupDto>, Dictionary<string, long>>()
            .ConvertUsing(src => src
                .ToDictionary(
                    group => group.Name.ToLower()
                        .Replace('c', 'с')
                        .Replace('s', 'с')
                        .Replace('v', 'в')
                        .Replace('r', 'р')
                        .Replace('m', 'м')
                        .Replace('k', 'к')
                        .Replace('a', 'а')
                        .Replace('e', 'е')
                        .Replace('o', 'о')
                        .Replace('p', 'р')
                        .Replace('x', 'ч')
                        .Replace('b', 'в'),
                    group => group.ItemId));
        
        CreateMap<List<TeacherDto>, Dictionary<long, Teacher>>()
            .ConvertUsing(src => src
                .ToDictionary(
                    teacher => teacher.ItemId, 
                    teacher => new Teacher() 
                    {
                        Name = teacher.Name,
                        AcademicTitle = teacher.AcademicTitle,
                        Degree = teacher.Degree,
                        Post = teacher.Post 
                    }));
        
        CreateMap<List<DepartmentDto>, Dictionary<long, string>>()
            .ConvertUsing(
                src => src
                    .ToDictionary(
                        department => department.ItemId, 
                        department => department.Name));
        
        CreateMap<VersionDto, Version>();
    }
}