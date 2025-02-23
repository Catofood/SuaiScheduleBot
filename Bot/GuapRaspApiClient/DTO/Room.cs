using System.Text.Json.Serialization;

namespace Bot.GuapRaspApiClient.DTO;
public record Room
{
    [JsonPropertyName("ItemId")] public int ItemId;
    [JsonPropertyName("Name")] public string Name;
    [JsonPropertyName("BuildingId")] public int BuildingId;
}


// /get-sem-rooms - получить список аудиторий
// Формат данных:
// [
//   ...
//     {'Name': number,         // Номер аудитории
//      'BuildingId': building_id,  // Идентификатор здания
//      'ItemId': gr_id         // Идентификатор аудитории
//   ...
// ]


// Пример 
// [
//     {
//         "ItemId": 1,
//         "Name": "23-12",
//         "BuildingId": 2
//     },
//     {
//         "ItemId": 2,
//         "Name": "23-09",
//         "BuildingId": 2
//     },
//  ]