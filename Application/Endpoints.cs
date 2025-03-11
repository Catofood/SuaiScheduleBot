using Microsoft.Extensions.Configuration;

namespace Application;

// Предназначение класса: Возвращать эндпоинты расписания ГУАП
public class Endpoints
{
    private readonly string DirectoryName = "Endpoints";
    private readonly IConfiguration _config;

    public Endpoints(IConfiguration config)
    {
        _config = config;
    }

    private string GetPath(string? endpoint)
    {
        var route = _config[$"{DirectoryName}:{endpoint}"];
        if (string.IsNullOrEmpty(endpoint))
            throw new Exception(
                $"Failed to retrieve the API URL from the configuration. Make sure the '{DirectoryName}:{endpoint}' key is present in appsettings.json.");
        var result = GetBaseApiUrl() + route;
        return result;
    }

    private string GetBaseApiUrl()
    {
        var url = _config[$"{DirectoryName}:Url"];
        if (string.IsNullOrEmpty(url))
            throw new Exception(
                $"Failed to retrieve the API URL from the configuration. Make sure the '{DirectoryName}:Url' key is present in appsettings.json.");
        return url;
    }

    public string Version => GetPath("Version");
    public string Rooms => GetPath("Rooms");
    public string Buildings => GetPath("Buildings");
    public string Departments => GetPath("Departments");
    public string Teachers => GetPath("Teachers");
    public string Groups => GetPath("Groups");
    public string StudyEvents => GetPath("StudyEvents");
    public string ExamEvents => GetPath("ExamEvents");
}