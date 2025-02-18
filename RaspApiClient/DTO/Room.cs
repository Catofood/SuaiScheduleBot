namespace RaspApiClient.DTO;

using System.Text.Json.Serialization;

public class Room
{
    [JsonPropertyName("ItemId")]
    public int ItemId { get; init; }

    [JsonPropertyName("Name")]
    public string Name { get; init; }

    [JsonPropertyName("BuildingId")]
    public int BuildingId { get; init; }
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