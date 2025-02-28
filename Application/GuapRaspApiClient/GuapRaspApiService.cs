using Application.GuapRaspApiClient;
using Application.GuapRaspApiClient.DTO;
using Microsoft.Extensions.Configuration;
using DTO_Version = Application.GuapRaspApiClient.DTO.Version;
using Version = Application.GuapRaspApiClient.DTO.Version;

namespace Application.Services;

using System.Text.Json;

public class GuapRaspApiService
{
    private readonly HttpClient _httpClient;

    public GuapRaspApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // Пример метода для получения данных с API
    private async Task<T> GetDataAsync<T>(string fullEndpointPath)
    {
        var response = await _httpClient.GetAsync(fullEndpointPath);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(jsonResponse);

        return data;
    }
    
    public async Task<DTO_Version> GetVersionAsync()
    {
        return await GetDataAsync<DTO_Version>(Endpoints.Version);
    }

    public async Task<List<Room>> GetRoomsAsync()
    {
        return await GetDataAsync<List<Room>>(Endpoints.Rooms);
    }

    public async Task<List<Building>> GetBuildingsAsync()
    {
        return await GetDataAsync<List<Building>>(Endpoints.Buildings);
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        return await GetDataAsync<List<Department>>(Endpoints.Departments);
    }

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        return await GetDataAsync<List<Teacher>>(Endpoints.Teachers);
    }

    public async Task<List<Group>> GetGroupsAsync()
    {
        return await GetDataAsync<List<Group>>(Endpoints.Groups);
    }

    
    // TODO:
    // Сейчас у WeeklySchedule все значения null, нужно исправить
    public async Task<Dictionary<int, JsonElement>> GetAllStudiesAsync()
    {
        var json = await GetDataAsync<JsonDocument>(Endpoints.StudyEvents);
        JsonElement schedulesJson = json.RootElement.GetProperty("groups");
        var schedules = schedulesJson.Deserialize<Dictionary<int, JsonElement>>();
        return schedules ?? throw new InvalidOperationException();
    }

    public async Task<Dictionary<int, WeeklySchedule>> GetAllExamsAsync()
    {
        var json = await GetDataAsync<JsonDocument>(Endpoints.StudyEvents);
        JsonElement schedulesJson = json.RootElement.GetProperty("groups");
        var schedules = schedulesJson.Deserialize<Dictionary<int, WeeklySchedule>>();
        return schedules ?? throw new InvalidOperationException();
    }
    // Пример:
    // {
    //     "groups": {
    //         "1": {
    //             "monday": [
    //             {
    //                 "eventName": "Бюджетный учет и отчетность",
    //                 "eventDateStart": 1739812200,
    //                 "eventDateEnd": 1739817600,
    //                 "roomIds": [
    //                 3
    //                     ],
    //                 "teacherIds": [
    //                 798
    //                     ],
    //                 "departmentId": 3,
    //                 "eventType": "Лекция"
    //             },
    //         }
    //      }
    // }
}