using Bot.GuapRaspApiClient;
using Bot.GuapRaspApiClient.DTO;
using Microsoft.Extensions.Configuration;
using Version = Bot.GuapRaspApiClient.DTO.Version;

namespace Bot.Services;

using System.Text.Json;

public class GuapRaspApiService
{
    private readonly HttpClient _httpClient;

    public GuapRaspApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Пример метода для получения данных с API
    private async Task<T> GetDataAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка при запросе данных: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(jsonResponse);

        return data;
    }
    
    public async Task<Version> GetVersionAsync()
    {
        return await GetDataAsync<Version>(Endpoints.Version);
    }

    public async Task<List<Room>> GetRoomsAsync()
    {
        return await GetDataAsync<List<Room>>(Endpoints.Rooms);
    }

    public async Task<List<Building>> GetBuildings()
    {
        return await GetDataAsync<List<Building>>(Endpoints.Buildings);
    }

    public async Task<List<Department>> GetDepartments()
    {
        return await GetDataAsync<List<Department>>(Endpoints.Departments);
    }

    public async Task<List<Teacher>> GetTeachers()
    {
        return await GetDataAsync<List<Teacher>>(Endpoints.Teachers);
    }

    public async Task<List<Groups>> GetGroups()
    {
        return await GetDataAsync<List<Groups>>(Endpoints.Groups);
    }

    // TODO: Проверить как там с вложенностью на guap rasp api
    public async Task<Lol> GetStudies()
    {
        return await GetDataAsync<Lol>(Endpoints.StudyEvents);
    }

    public async Task<Lol> GetExams()
    {
        return await GetDataAsync<Lol>(Endpoints.ExamEvents);
    }
}