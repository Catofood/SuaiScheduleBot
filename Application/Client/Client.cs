using Application.Client.DTO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Version = Application.Client.DTO.Version;

namespace Application.Client;


public class Client 
{
    public Client(IHttpClientFactory httpClientFactory, Endpoints endpoints)
    {
        _endpoints = endpoints;
        _httpClient = httpClientFactory.CreateClient();
    }

    private readonly HttpClient _httpClient;
    private readonly Endpoints _endpoints;
    
    private async Task<string> GetDataAsync(string fullEndpointPath)
    {
        var response = await _httpClient.GetAsync(fullEndpointPath);
        if (!response.IsSuccessStatusCode) throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }
    
    public async Task<List<Teacher>> GetTeachers()
    {
        var jsonStr = await GetDataAsync(_endpoints.Teachers);
        var teacher = JsonConvert.DeserializeObject<List<Teacher>>(jsonStr);
        return teacher;
    }

    public async Task<Dictionary<long, WeeklySchedule>> GetExamEvents()
    {
        var jsonStr = await GetDataAsync(_endpoints.ExamEvents);
        var studyEvents = JToken.Parse(jsonStr)["groups"].ToObject<Dictionary<long, WeeklySchedule>>();
        return studyEvents;
    }

    // TODO: Написать тесты
    public async Task<Dictionary<long, WeeklySchedule>?> GetStudyEvents()
    {
        var jsonStr = await GetDataAsync(_endpoints.StudyEvents);
        var studyEvents = JToken.Parse(jsonStr)["groups"].ToObject<Dictionary<long, WeeklySchedule>>();
        return studyEvents;
    }

    public async Task<List<Group>> GetGroups()
    {
        var jsonStr = await GetDataAsync(_endpoints.Groups);
        var groups = JsonConvert.DeserializeObject<List<Group>>(jsonStr);
        return groups;
    }

    public async Task<List<Department>> GetDepartments()
    {
        var jsonStr = await GetDataAsync(_endpoints.Departments);
        var departments = JsonConvert.DeserializeObject<List<Department>>(jsonStr);
        return departments;
    }

    public async Task<List<Building>> GetBuildings()
    {
        var jsonStr = await GetDataAsync(_endpoints.Buildings);
        var buildings = JsonConvert.DeserializeObject<List<Building>>(jsonStr);
        return buildings;
    }

    public async Task<List<Room>> GetRooms()
    {
        var jsonStr = await GetDataAsync(_endpoints.Rooms);
        var rooms = JsonConvert.DeserializeObject<List<Room>>(jsonStr);
        return rooms;
    }

    public async Task<Version> GetVersion()
    {
        var jsonStr = await GetDataAsync(_endpoints.Version);
        var version = JsonConvert.DeserializeObject<Version>(jsonStr);
        return version;
    }
}