using Application.Cache.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using Version = Application.Cache.Entities.Version;

namespace Application.Cache;

public class CacheRepository // : ICacheRepository
{
    readonly IDatabase _db;
    public CacheRepository(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetBuildingName(string name, long id)
    {
        await _db.StringSetAsync($"building:{id}", name);
    }
    
    public async Task<string?> GetBuildingName(long id)
    {
        var data = await _db.StringGetAsync($"building:{id}");
        return data.ToString();
    }

    public async Task SetRoom(Room room, long id)
    {
        await _db.StringSetAsync($"room:{id}", JsonConvert.SerializeObject(room));
    }
    
    public async Task<Room?> GetRoom(long id)
    {
        var data = await _db.StringGetAsync($"room:{id}");
        return data.HasValue ? JsonConvert.DeserializeObject<Room>(data.ToString()) : null;
    }

    public async Task SetDepartmentName(string name, long id)
    {
        await _db.StringSetAsync($"department:{id}", name);
    }

    public async Task<string?> GetDepartmentName(long id)
    {
        var data = await _db.StringGetAsync($"department:{id}");
        return data.ToString();
    }

    public async Task SetGroupId(string name, long id)
    {
        await _db.StringSetAsync($"group:{name}", id);
    }
    
    public async Task<long?> GetGroupId(string name)
    {
        var data = await _db.StringGetAsync($"group:{name}");
        return data.HasValue ? long.Parse(data.ToString()) : null;
    }

    public async Task SetTeacher(Teacher teacher, long id)
    {
        await _db.StringSetAsync($"teacher:{id}", JsonConvert.SerializeObject(teacher));
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
}