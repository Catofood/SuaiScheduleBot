using Application.Cache;
using Application.Client.DTO;
using Application.Extensions;
using Application.Models;

namespace Application.Mapping;

public class AsyncMapperService
{
    private readonly CacheService _cacheService;

    public AsyncMapperService(CacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Pair> ConvertEventDtoToPair(EventDto eventDto)
    {
        var pair = new Pair();
        
        var room = await _cacheService.GetRoom(eventDto.RoomIds.FirstOrDefault());
        if (room != null)
        {
            pair.Room = room.Name;
            pair.Building = await _cacheService.GetBuildingName(room.BuildingId);
        }
        
        var teacher = await _cacheService.GetTeacher(eventDto.TeacherIds.FirstOrDefault());
        if (teacher != null)
        {
            pair.TeacherName = teacher.Name;
            pair.TeacherPost = teacher.Post;
        }
        
        var pairStartTime = eventDto.EventDateStart;
        pair.PairStartTime = pairStartTime.HasValue ? DateTimeOffset.FromUnixTimeSeconds((long)pairStartTime!) : null;
        
        pair.EventType = eventDto.EventType;
        pair.EventName = eventDto.EventName;
        
        pair.DepartmentName = await _cacheService.GetDepartmentName(eventDto.DepartmentId);
        return pair;
    }

    public async Task<List<Pair>> ConvertEventDtosToPairs(List<EventDto> eventDtos)
    {
        var tasks = eventDtos.Select(ConvertEventDtoToPair);
        return (await Task.WhenAll(tasks)).ToList();
    }
}