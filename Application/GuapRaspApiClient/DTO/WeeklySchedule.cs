using System.Text.Json.Serialization;

namespace Application.GuapRaspApiClient.DTO;

public class WeeklySchedule
{
    [JsonPropertyName("monday")] public List<Event> Monday;
    [JsonPropertyName("tuesday")] public List<Event> Tuesday;
    [JsonPropertyName("wednesday")] public List<Event> Wednesday;
    [JsonPropertyName("thursday")] public List<Event> Thursday;
    [JsonPropertyName("friday")] public List<Event> Friday;
    [JsonPropertyName("saturday")] public List<Event> Saturday;
    [JsonPropertyName("other")] public List<Event> Other;
}


//
// Формат:
// {
//     day_name:
//     [
//     {
//         "eventName": disc_name,
//         "eventDateStart": datetime_timestamp,
//         "eventDateEnd": datetime_timestamp,
//         "eventType": lesson_type_full,
//         "teacherIds": [teacher_id,...],
//         "groupIds": [group_id,...],
//     },
//     ...
//         ]
// }