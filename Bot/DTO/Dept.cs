using System.Text.Json.Serialization;

namespace Bot.DTO;

public record Dept
{
    [JsonPropertyName("ItemId")] public int ItemId;
    [JsonPropertyName("Name")] public string Name;
}


// /get-sem-depts - получить список кафедр
// Формат данных:
// [
//   ...
//     {'Name': dept_num,      // Номер кафедры
//      'ItemId': dept_id      // Идентификатор кафедры
//   ...
// ]


// Пример ответа от API на запрос /get-sem-rooms
// Формат данных:
// [
//   {
//     "ItemId": 38,    // Идентификатор аудитории
//     "Name": "1"      // Номер аудитории
//   },
//   {
//     "ItemId": 37,    // Идентификатор аудитории
//     "Name": "13"     // Номер аудитории
//   }
// ]