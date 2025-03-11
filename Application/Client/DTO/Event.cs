using Newtonsoft.Json;

namespace Application.Client.DTO;

public class Event 
{
    [JsonProperty("eventName")] 
    public string EventName;
    [JsonProperty("eventDateStart")] 
    public long? EventDateStart;
    [JsonProperty("eventDateEnd")] 
    public long? EventDateEnd;
    [JsonProperty("roomIds")] 
    public List<long> RoomIds;
    [JsonProperty("teacherIds")] 
    public List<long> TeacherIds;
    [JsonProperty("departmentId")] 
    public long DepartmentId;
    [JsonProperty("eventType")] 
    public string EventType;
}


// /get-sem-events - получить список пар очки и вечерки по группам и дням
// Формат:
// {\
//     "groups":
//     {
//         gr_id:
//         {
//             day_name:
//             [
//             {
//                 "eventName": disc_name,
//                 "eventDateStart": datetime_timestamp,
//                 "eventDateEnd": datetime_timestamp,
//                 "eventType": lesson_type_full,
//                 "roomIds": [room_id,...],
//                 "teacherIds": [teacher_id,...],
//                 "departmentId": department_id, (при наличии)
//             },
//             ...
//                 ]
//         }
//     }
// }
