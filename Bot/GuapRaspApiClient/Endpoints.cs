using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Bot.GuapRaspApiClient;

// Предназначение класса: Возвращать эндпоинты расписания гуап
public static class Endpoints
{
    // TODO: Добавить автоматическую проверку перед
    // запуском проекта на наличие этих данных.
    // (Сделать get запросы)
    public static string GetGuapRaspApiUrl = "https://test-rasp.guap.ru:8080";
    
    public static string Version
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-version");
            return stringBuilder.ToString();
        }
    }

    public static string Rooms
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-rooms");
            return stringBuilder.ToString();
        }
    }
    
    public static string Buildings
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-builds");
            return stringBuilder.ToString();
        }
    }

    public static string Departments
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-depts");
            return stringBuilder.ToString();
        }
    }

    public static string Teachers
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-teachers");
            return stringBuilder.ToString();
        }
    }

    public static string Groups
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-groups");
            return stringBuilder.ToString();
        }
    }

    public static string StudyEvents
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-sem-events");
            return stringBuilder.ToString();
        }
    }

    public static string ExamEvents
    {
        get
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetGuapRaspApiUrl);
            stringBuilder.Append("/get-session-events");
            return stringBuilder.ToString();
        }
    }

    public static string GroupExamEvents(int groupId)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(GetGuapRaspApiUrl);
        stringBuilder.Append($"/get-session-group-events?id={groupId.ToString()}");
        return stringBuilder.ToString();
    }

    // term - семестр (1 или 2)
    public static string GroupStudyEvents(int term, int groupId, long? dateStart = null, long? dateEnd = null)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(GetGuapRaspApiUrl);
        stringBuilder.Append($"/get-sem-group-events?term={term.ToString()}&id={groupId.ToString()}");
        if (dateStart.HasValue) stringBuilder.Append($"&startdate={dateStart.ToString()}");
        if (dateEnd.HasValue) stringBuilder.Append($"&enddate={dateStart.ToString()}");
        return stringBuilder.ToString();
    }
}