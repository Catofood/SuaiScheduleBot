using System.Text.Json.Serialization;

namespace RaspApiClient.DTO;


public class Schedule
{
    [JsonPropertyName("monday")]
    public List<Event> Monday { get; init; }

    [JsonPropertyName("tuesday")]
    public List<Event> Tuesday { get; init; }

    [JsonPropertyName("wednesday")]
    public List<Event> Wednesday { get; init; }

    [JsonPropertyName("thursday")]
    public List<Event> Thursday { get; init; }

    [JsonPropertyName("friday")]
    public List<Event> Friday { get; init; }

    [JsonPropertyName("saturday")]
    public List<Event> Saturday { get; init; }

    [JsonPropertyName("other")]
    public List<Event> Other { get; init; }
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