using System.Text.Json.Serialization;

namespace Bot.AppSettings;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; init; }
    public GuapRaspApi GuapRaspApi { get; init; }

    public AppSettings(ConnectionStrings connectionStrings, GuapRaspApi guapRaspApi)
    {
        ConnectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
        GuapRaspApi = guapRaspApi ?? throw new ArgumentNullException(nameof(guapRaspApi));
    }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; init; }

    public ConnectionStrings(string defaultConnection)
    {
        DefaultConnection = defaultConnection ?? throw new ArgumentNullException(nameof(defaultConnection));
    }
}

public class GuapRaspApi
{
    // TODO: Добавить остальные роуты сюда и в appsettings.json
    [JsonPropertyName("Url")]
    public string Url { get; init; }

    [JsonPropertyName("GetVersionRoute")]
    public string GetVersionRoute { get; init; }

    [JsonPropertyName("GetAllRoomsRoute")]
    public string GetAllRoomsRoute { get; init; }

    [JsonPropertyName("GetAllBuildingsRoute")]
    public string GetAllBuildingsRoute { get; init; }

    [JsonPropertyName("GetAllDepartmentsRoute")]
    public string GetAllDepartmentsRoute { get; init; }

    [JsonPropertyName("GetAllTeachersRoute")]
    public string GetAllTeachersRoute { get; init; }

    [JsonPropertyName("GetAllGroupsRoute")]
    public string GetAllGroupsRoute { get; init; }

    [JsonPropertyName("GetAllStudiesRoute")]
    public string GetAllStudiesRoute { get; init; }

    [JsonPropertyName("GetAllExamsRoute")]
    public string GetAllExamsRoute { get; init; }

    [JsonPropertyName("GetExamsOfGroupRoute")]
    public string GetExamsOfGroupRoute { get; init; }

    [JsonPropertyName("GetStudiesOfGroupRoute")]
    public string GetStudiesOfGroupRoute { get; init; }

    public GuapRaspApi(
        string url,
        string getVersionRoute,
        string getAllRoomsRoute,
        string getAllBuildingsRoute,
        string getAllDepartmentsRoute,
        string getAllTeachersRoute,
        string getAllGroupsRoute,
        string getAllStudiesRoute,
        string getAllExamsRoute,
        string getExamsOfGroupRoute,
        string getStudiesOfGroupRoute)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        GetVersionRoute = getVersionRoute ?? throw new ArgumentNullException(nameof(getVersionRoute));
        GetAllRoomsRoute = getAllRoomsRoute ?? throw new ArgumentNullException(nameof(getAllRoomsRoute));
        GetAllBuildingsRoute = getAllBuildingsRoute ?? throw new ArgumentNullException(nameof(getAllBuildingsRoute));
        GetAllDepartmentsRoute = getAllDepartmentsRoute ?? throw new ArgumentNullException(nameof(getAllDepartmentsRoute));
        GetAllTeachersRoute = getAllTeachersRoute ?? throw new ArgumentNullException(nameof(getAllTeachersRoute));
        GetAllGroupsRoute = getAllGroupsRoute ?? throw new ArgumentNullException(nameof(getAllGroupsRoute));
        GetAllStudiesRoute = getAllStudiesRoute ?? throw new ArgumentNullException(nameof(getAllStudiesRoute));
        GetAllExamsRoute = getAllExamsRoute ?? throw new ArgumentNullException(nameof(getAllExamsRoute));
        GetExamsOfGroupRoute = getExamsOfGroupRoute ?? throw new ArgumentNullException(nameof(getExamsOfGroupRoute));
        GetStudiesOfGroupRoute = getStudiesOfGroupRoute ?? throw new ArgumentNullException(nameof(getStudiesOfGroupRoute));
    }
}
