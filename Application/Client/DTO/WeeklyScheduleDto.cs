using Newtonsoft.Json;

namespace Application.Client.DTO;

public class WeeklyScheduleDto
{
    [JsonProperty("monday")] 
    public List<EventDto> Monday = new();
    [JsonProperty("tuesday")] 
    public List<EventDto> Tuesday = new();
    [JsonProperty("wednesday")] 
    public List<EventDto> Wednesday = new();
    [JsonProperty("thursday")] 
    public List<EventDto> Thursday = new();
    [JsonProperty("friday")] 
    public List<EventDto> Friday = new();
    [JsonProperty("saturday")] 
    public List<EventDto> Saturday = new();
    [JsonProperty("other")] 
    public List<EventDto> Other = new();
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
//         "departmentId": department_id, (при наличии)
//     },
//     ...
//         ]
// }