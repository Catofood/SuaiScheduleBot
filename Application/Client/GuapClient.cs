using Application.Client.DTO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Client;


public class GuapClient 
{
    public GuapClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    private readonly HttpClient _httpClient;
    
    private async Task<string> GetDataAsync(string fullEndpointPath)
    {
        var response = await _httpClient.GetAsync(fullEndpointPath);
        if (!response.IsSuccessStatusCode) throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }
    
    public async Task<List<TeacherDto>> GetTeachers()
    {
        var jsonStr = await GetDataAsync(Endpoints.Teachers);
        var teacher = JsonConvert.DeserializeObject<List<TeacherDto>>(jsonStr);
        return teacher;
    }

    public async Task<List<GroupDto>> GetGroups()
    {
        var jsonStr = await GetDataAsync(Endpoints.Groups);
        var groups = JsonConvert.DeserializeObject<List<GroupDto>>(jsonStr);
        return groups;
    }

    public async Task<List<DepartmentDto>> GetDepartments()
    {
        var jsonStr = await GetDataAsync(Endpoints.Departments);
        var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsonStr);
        return departments;
    }

    public async Task<List<BuildingDto>> GetBuildings()
    {
        var jsonStr = await GetDataAsync(Endpoints.Buildings);
        var buildings = JsonConvert.DeserializeObject<List<BuildingDto>>(jsonStr);
        return buildings;
    }

    public async Task<List<RoomDto>> GetRooms()
    {
        var jsonStr = await GetDataAsync(Endpoints.Rooms);
        var rooms = JsonConvert.DeserializeObject<List<RoomDto>>(jsonStr);
        return rooms;
    }

    public async Task<VersionDto> GetVersion()
    {
        var jsonStr = await GetDataAsync(Endpoints.Version);
        var version = JsonConvert.DeserializeObject<VersionDto>(jsonStr);
        return version;
    }
    
    public async Task<Dictionary<long, WeeklyScheduleDto>> GetGroupExamEvents(long groupId, DateTimeOffset? startdate = null, DateTimeOffset? enddate = null)
    {
        var json = await GetDataAsync(Endpoints.GetExamEvents(groupId, startdate, enddate));
        return await ParseGroupEventsAsync(json);
    }

    public async Task<Dictionary<long, WeeklyScheduleDto>> GetGroupStudyEvents(long groupId, DateTimeOffset? startdate = null, DateTimeOffset? enddate = null)
    {
        var json = await GetDataAsync(Endpoints.GetStudyEvents(groupId, startdate, enddate));
        return await ParseGroupEventsAsync(json);
    }

    public async Task<Dictionary<long, WeeklyScheduleDto>> GetAllExamEvents(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        var json = await GetDataAsync(Endpoints.GetExamEvents(startDate: startDate, endDate: endDate));
        return await ParseAllEventsAsync(json);
    }

    public async Task<Dictionary<long, WeeklyScheduleDto>?> GetAllStudyEvents()
    {
        var json = await GetDataAsync(Endpoints.GetStudyEvents());
        return await ParseAllEventsAsync(json);
    }

    private async Task<Dictionary<long, WeeklyScheduleDto>?> ParseAllEventsAsync(string endpoint)
    {
        var jsonStr = await GetDataAsync(endpoint);
        return JToken.Parse(jsonStr)["groups"]?.ToObject<Dictionary<long, WeeklyScheduleDto>>() ?? new();
    }
    private async Task<Dictionary<long, WeeklyScheduleDto>> ParseGroupEventsAsync(string json)
    {
        return JToken.Parse(json).ToObject<Dictionary<long, WeeklyScheduleDto>>() ?? new();
    }
    
}