using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application;

// Предназначение класса: Возвращать эндпоинты расписания ГУАП
public static class Endpoints
{
    private const string BaseUrl = "https://test-rasp.guap.ru:8080";
    public const string Version = BaseUrl + "/get-version?";
    public const string Rooms = BaseUrl + "/get-sem-rooms?";
    public const string Buildings = BaseUrl + "/get-sem-builds?";
    public const string Departments = BaseUrl + "/get-sem-depts?";
    public const string Teachers = BaseUrl + "/get-sem-teachers?";
    public const string Groups = BaseUrl + "/get-sem-groups?";
    private const string AllStudyEvents = BaseUrl + "/get-sem-events?";
    private const string AllExamEvents = BaseUrl + "/get-session-events?";
    private const string GroupStudyEvents = BaseUrl + "/get-sem-group-events?";
    private const string GroupExamEvents = BaseUrl + "/get-session-group-events?";

    public static string GetStudyEvents(long? groupId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        if (groupId == null) return AllStudyEvents + CreateQuery(startDate: startDate, endDate: endDate);
        return GroupStudyEvents + CreateQuery(groupId, startDate, endDate);
    }

    public static string GetExamEvents(long? groupId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        if (groupId == null) return AllExamEvents + CreateQuery(startDate: startDate, endDate: endDate);
        return GroupExamEvents + CreateQuery(groupId, startDate, endDate);
    }

    private static string CreateQuery(long? groupId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        var stringBuilder = new StringBuilder();
        if (groupId.HasValue) stringBuilder.Append($"&id={groupId}");
        if (startDate.HasValue) stringBuilder.Append($"&startdate={startDate.Value.ToUnixTimeSeconds()}");
        if (endDate.HasValue) stringBuilder.Append($"&enddate={endDate.Value.ToUnixTimeSeconds()}");
        return stringBuilder.ToString();
    }
}