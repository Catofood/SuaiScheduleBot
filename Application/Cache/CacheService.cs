using Application.Cache.Entities;
using Application.Client;
using Application.Extensions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Version = Application.Cache.Entities.Version;

namespace Application.Cache;

public class CacheService // : ICacheRepository
{
    readonly IDatabase _db;
    readonly SuaiClient _suaiClient;
    readonly IMapper _mapper;
    public CacheService(IConnectionMultiplexer connection, SuaiClient suaiClient, IMapper mapper)
    {
        _suaiClient = suaiClient;
        _mapper = mapper;
        _db = connection.GetDatabase();
    }

    public async Task FlushDb()
    {
        await _db.ExecuteAsync("FLUSHDB");
    }
    
    public async Task SetBuildingName(long id, string buildingName)
    {
        await _db.StringSetAsync($"building:{id}", buildingName);
    }
    
    public async Task SetBuildingNames(Dictionary<long, string> buildingNames)
    {
        foreach (var kv in buildingNames) await SetBuildingName(kv.Key, kv.Value);
    }
    
    public async Task<string?> GetBuildingName(long id)
    {
        var data = await _db.StringGetAsync($"building:{id}");
        return data.ToString();
    }

    public async Task SetRoom(long id, Room room)
    {
        await _db.StringSetAsync($"room:{id}", JsonConvert.SerializeObject(room));
    }

    public async Task SetRooms(Dictionary<long, Room> rooms)
    {
        foreach (var kv in rooms) await SetRoom(kv.Key, kv.Value);
    }
    
    public async Task<Room?> GetRoom(long id)
    {
        var data = await _db.StringGetAsync($"room:{id}");
        return data.HasValue ? JsonConvert.DeserializeObject<Room>(data.ToString()) : null;
    }

    public async Task SetDepartmentName(long id, string departmentName)
    {
        await _db.StringSetAsync($"department:{id}", departmentName);
    }

    public async Task SetDepartmentNames(Dictionary<long, string> departmentNames)
    {
        foreach (var kv in departmentNames) await SetDepartmentName(kv.Key, kv.Value);
    }

    public async Task<string?> GetDepartmentName(long id)
    {
        var data = await _db.StringGetAsync($"department:{id}");
        return data.ToString();
    }

    public async Task SetGroupId(string name, long id)
    {
        await _db.StringSetAsync($"group:{name.ToLowerRussian()}", id);
    }

    public async Task SetGroupIds(Dictionary<string, long> idByNames)
    {
        foreach (var kv in idByNames) await SetGroupId(kv.Key, kv.Value);
    }

    public async Task<long?> GetGroupId(string name)
    {
        var data = await _db.StringGetAsync($"group:{name.ToLowerRussian()}");
        return data.HasValue ? long.Parse(data.ToString()) : null;
    }

    public async Task SetTeacher(long id, Teacher teacher)
    {
        await _db.StringSetAsync($"teacher:{id}", JsonConvert.SerializeObject(teacher));
    }

    public async Task SetTeachers(Dictionary<long, Teacher> teachers)
    {
        foreach (var kv in teachers) await SetTeacher(kv.Key, kv.Value);
    }

    public async Task<Teacher?> GetTeacher(long id)
    {
        var data = await _db.StringGetAsync($"teacher:{id}");
        return data.HasValue ? JsonConvert.DeserializeObject<Teacher>(data.ToString()) : null;
    }

    public async Task SetVersion(Version version)
    {
        await _db.StringSetAsync($"version", JsonConvert.SerializeObject(version));
    }
    
    public async Task<Version?> GetVersion()
    {
        var data = await _db.StringGetAsync("version");
        return data.HasValue ? JsonConvert.DeserializeObject<Version>(data.ToString()) : null;
    }
    
    // Этот метод ПОЛНОСТЬЮ очищает redis, затем выполняет запросы на апи гуап и сохраняет данные в redis
    public async Task UpdateCache()
    {
        await FlushDb();
        await Task.WhenAll(DoBuildings(), DoRooms(), DoGroups(), DoTeachers(), DoDepartments(), DoVersion());
        // Делаем все нужные запросы на API гуап и сохраняем данные в кэш
        async Task DoBuildings()
        {
            var buildingDtos = await _suaiClient.GetBuildings();
            var buildingEntities = _mapper.Map<Dictionary<long, string>>(buildingDtos);
            await SetBuildingNames(buildingEntities);
        }

        async Task DoRooms()
        {
            var roomsDtos = await _suaiClient.GetRooms();
            var roomEntities = _mapper.Map<Dictionary<long, Room>>(roomsDtos);
            await SetRooms(roomEntities);
        }

        async Task DoGroups()
        {
            var groupDtos = await _suaiClient.GetGroups();
            var groupEntities = _mapper.Map<Dictionary<string, long>>(groupDtos);
            await SetGroupIds(groupEntities);
        }

        async Task DoTeachers()
        {
            var teacherDtos = await _suaiClient.GetTeachers();
            var teacherEntities = _mapper.Map<Dictionary<long, Teacher>>(teacherDtos);
            await SetTeachers(teacherEntities);
        }


        async Task DoDepartments()
        {
            var departmentDtos = await _suaiClient.GetDepartments();
            var departmentEntities = _mapper.Map<Dictionary<long, string>>(departmentDtos);
            await SetDepartmentNames(departmentEntities);
        }

        async Task DoVersion()
        {
            var versionDto = await _suaiClient.GetVersion();
            var versionEntity = _mapper.Map<Cache.Entities.Version>(versionDto);
            await SetVersion(versionEntity);
        }
    }
}