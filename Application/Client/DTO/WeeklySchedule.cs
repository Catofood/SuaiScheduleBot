using Newtonsoft.Json;

namespace Application.Client.DTO;

public class WeeklySchedule
{
    [JsonProperty("monday")] 
    public List<Event> Monday;
    [JsonProperty("tuesday")] 
    public List<Event> Tuesday;
    [JsonProperty("wednesday")] 
    public List<Event> Wednesday;
    [JsonProperty("thursday")] 
    public List<Event> Thursday;
    [JsonProperty("friday")] 
    public List<Event> Friday;
    [JsonProperty("saturday")] 
    public List<Event> Saturday;
    [JsonProperty("other")] 
    public List<Event> Other;
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