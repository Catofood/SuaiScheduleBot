using System.Text.Json;

namespace RaspApiClient;

public static class Routes
{
    internal const string ApiUrl = "https://test-rasp.guap.ru:8080";
    internal const string GetRoomsRoute = "/get-sem-rooms";
    internal const string GetTeachersRoute = "/get-sem-teachers";
    internal const string GetGroupsRoute = "/get-sem-groups";
    internal const string GetSessionEventsByGroup = "/get-session-group-events?id=<group_id>";
    internal const string GetEventsByGroup = "/get-sem-group-events?term=<term_num>&id=<group_id>&startdate=&enddate=";
}