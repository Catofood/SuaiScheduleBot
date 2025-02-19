namespace RaspApiClient.DTO;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Event
{
    [JsonPropertyName("eventName")] public string EventName { get; init; }

    [JsonPropertyName("eventDateStart")] public long EventDateStart { get; init; }

    [JsonPropertyName("eventDateEnd")] public long EventDateEnd { get; init; }

    [JsonPropertyName("roomIds")] public List<int> RoomIds { get; init; }

    [JsonPropertyName("teacherIds")] public List<int> TeacherIds { get; init; }

    [JsonPropertyName("departmentId")] public int DepartmentId { get; init; }

    [JsonPropertyName("eventType")] public string EventType { get; init; }
}


// /get-sem-group-events?term=<term_num>&id=<group_id>&startdate=&enddate= - получить список пар (очки и вечерки) для группы с ИД group_id
// Аргументы:
//   term - номер семестра (1 - осень, 2 - весна)
//   id - ИД группы (внутренний, можно получить из /get-sem-groups)
//   startdate - начало временного отрезка (опционально)
//   enddate - конец временного отрезка (опционально)

// Формат данных:
// {
//   day_name:                // Название дня недели (например, "Понедельник")
//   [
//     {
//       "eventName": disc_name,          // Название дисциплины (например, "Математика")
//       "eventDateStart": datetime_timestamp, // Время начала занятия в формате Unix timestamp
//       "eventDateEnd": datetime_timestamp,   // Время окончания занятия в формате Unix timestamp
//       "eventType": lesson_type_full,       // Тип занятия (например, "Лекция")
//       "roomIds": [room_id, ...],       // Список идентификаторов аудиторий
//       "teacherIds": [teacher_id, ...], // Список идентификаторов преподавателей
//     },
//     ...
//   ]
// }

/*
{
   "monday": [
       {
           "eventName": "Учебная практика",
           "eventDateStart": 1739175000,
           "eventDateEnd": 1739180400,
           "roomIds": [
               137
           ],
           "teacherIds": [
               259
           ],
           "eventType": "Практическая работа"
       },
       {
           "eventName": "Учебная практика",
           "eventDateStart": 1740384600,
           "eventDateEnd": 1740390000,
           "roomIds": [
               137
           ],
           "teacherIds": [
               259
           ],
           "eventType": "Практическая работа"
       }
   ],
   "tuesday": [
       {
           "eventName": "Учебная практика",
           "eventDateStart": 1739175000,
           "eventDateEnd": 1739180400,
           "roomIds": [
               137
           ],
           "teacherIds": [
               259
           ],
           "eventType": "Практическая работа"
       },
       {
           "eventName": "Учебная практика",
           "eventDateStart": 1740384600,
           "eventDateEnd": 1740390000,
           "roomIds": [
               137
           ],
           "teacherIds": [
               259
           ],
           "eventType": "Практическая работа"
       }
   ],
   "other": [
    {
        "eventName": "Психология (альтернативная по выбору)",
        "eventDateStart": null,
        "eventDateEnd": null,
        "roomIds": [],
        "teacherIds": [],
        "department": 22,
        "eventType": "Практическая работа"
    },
}
 */