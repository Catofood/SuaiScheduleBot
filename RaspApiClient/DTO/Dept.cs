namespace RaspApiClient.DTO;

using System.Text.Json.Serialization;

public class Dept
{
    [JsonPropertyName("ItemId")]
    public int ItemId { get; init; }
    
    [JsonPropertyName("Name")]
    public string Name { get; init; }
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
